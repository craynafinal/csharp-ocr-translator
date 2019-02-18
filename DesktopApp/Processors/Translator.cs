using BackgroundApp;
using DesktopApp.Forms;
using DesktopApp.Poco;
using DesktopApp.Pocos;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace DesktopApp.Processors
{
    /// <summary>
    /// This class read text from desktop and return translated text.
    /// </summary>
    class Translator
    {
        private PapagoTest papagoTest = new PapagoTest();
        private Thread translationThread;
        private static Translator instance;

        private Translator()
        {   
        }

        /// <summary>
        /// Get translator instance.
        /// </summary>
        /// <returns>Translator instance</returns>
        public static Translator GetInstance()
        {
            if (instance == null)
            {
                instance = new Translator();
            }

            return instance;
        }

        /// <summary>
        /// Kill the translation thread.
        /// </summary>
        public void Abort()
        {
            if (translationThread != null)
            {
                translationThread.Abort();
            }
        }

        /// <summary>
        /// Run translation in a separate thread.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="output"></param>
        public void Run(Configuration configuration, Output output)
        {
            if (translationThread != null && translationThread.IsAlive)
            {
                return;
            }

            translationThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    var task = TranslateBitmapText(ImageGrabber.ReadFromDesktop(configuration), configuration);
                    task.Wait();
                    output.SetTextBox(task.Result);
                    Thread.Sleep(500);
                }
            });

            translationThread.Start();
        }

        private async Task<string> TranslateBitmapText(DesktopBitmapData desktopBitmapData, Configuration configuration)
        {
            var language = configuration.GetSourceLanguage();
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new Exception($"{ language.LanguageTag } is not supported in this system.");
            }

            desktopBitmapData.ConvertBitmapToGrayscale();
            desktopBitmapData.AdjustBitmapBrightness(configuration.Brightness);
            desktopBitmapData.AdjustBitmapContrast(configuration.Contrast);
            desktopBitmapData.ApplyBitmapToMemory();

            var decoder = await BitmapDecoder.CreateAsync(desktopBitmapData.MemoryStream.AsRandomAccessStream());
            var softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            var engine = OcrEngine.TryCreateFromLanguage(language);
            var ocrResult = await engine.RecognizeAsync(softwareBitmap).AsTask();

            string result = papagoTest.Translate(Dictionary.GetInstance().Apply(ocrResult.Text), configuration.SourceLanguage, configuration.TargetLanguage);
            desktopBitmapData.Graphics.Flush();

            return result;
        }
    }
}
