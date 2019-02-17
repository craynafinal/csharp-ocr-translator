using BackgroundApp;
using DesktopApp.Filters;
using DesktopApp.Forms;
using DesktopApp.Poco;
using DesktopApp.Pocos;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
            translationThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    output.SetTextBox(TranslateBitmapText(ReadFromDesktop(configuration), configuration).Result);
                    Thread.Sleep(2000);
                }
            });

            translationThread.Start();
        }

        private DesktopBitmapData ReadFromDesktop(Configuration configuration)
        {
            if (!configuration.IsScreenshotAreaSet)
            {
                throw new Exception("Initialize screen focus first!");
            }

            MemoryStream memoryStream = new MemoryStream();

            var bitmap = new Bitmap(configuration.ScreenshotWidth, configuration.ScreenshotHeight, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            //Drawing.GetInstance().HideGraphic();

            graphics.CopyFromScreen(configuration.ScreenshotX, configuration.ScreenshotY, 0, 0,
                new Size(configuration.ScreenshotWidth, configuration.ScreenshotHeight), CopyPixelOperation.SourceCopy);

            //Drawing.GetInstance().Redraw(configuration);

            ImageFilter.ConvertBitmapToGrayscale(bitmap);
            ImageFilter.AdjustBitmapBrightness(bitmap, -50);
            ImageFilter.AdjustBitmapContrast(bitmap, 50);

            bitmap.Save(memoryStream, ImageFormat.Bmp);

            // TODO: for debugging purpose, remove it later
            bitmap.Save("D:\\Data\\Downloads\\Temp\\temp\\debugging.jpg");

            DesktopBitmapData bitmapData = new DesktopBitmapData();
            bitmapData.Bitmap = bitmap;
            bitmapData.Graphics = graphics;
            bitmapData.MemoryStream = memoryStream;

            return bitmapData;
        }

        private async Task<string> TranslateBitmapText(DesktopBitmapData desktopBitmapData, Configuration configuration)
        {
            var language = configuration.GetSourceLanguage();
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new Exception($"{ language.LanguageTag } is not supported in this system.");
            }

            var decoder = await BitmapDecoder.CreateAsync(desktopBitmapData.MemoryStream.AsRandomAccessStream());
            var softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            var engine = OcrEngine.TryCreateFromLanguage(language);
            var ocrResult = await engine.RecognizeAsync(softwareBitmap).AsTask();

            string result = papagoTest.Translate(Dictionary.GetInstance().Apply(ocrResult.Text), configuration.SourceLanguage, configuration.TargetLanguage);
            desktopBitmapData.Graphics.Flush();

            return ocrResult.Text + "\n" + result;
        }
    }
}
