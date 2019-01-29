using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace DesktopApp
{
    public partial class Form1 : Form
    {
        BackgroundApp.PapagoTest papagoTest = new BackgroundApp.PapagoTest();

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            test_bg_read();
        }

        private async void test_bg_read()
        {
            var language = new Language("ko");
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new Exception($"{ language.LanguageTag } is not supported in this system.");
            }

            using (MemoryStream m = new MemoryStream())
            {
                var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                Graphics.FromImage(bitmap).CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                bitmap.Save("D:\\Data\\Downloads\\Temp\\temp\\ddd2.jpg");
                bitmap.Save(m, ImageFormat.Bmp);
                
                var decoder = await BitmapDecoder.CreateAsync(m.AsRandomAccessStream());
                var softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                var engine = OcrEngine.TryCreateFromLanguage(language);
                var ocrResult = await engine.RecognizeAsync(softwareBitmap).AsTask();

                //papagoTest.Translate(ocrResult.Text.Replace("{", "").Replace("}", "").Replace(",", "").Replace(";", "").Replace(":", ""), "en", "ko");


                //TODO: url breaks because of special characters

                var watch = System.Diagnostics.Stopwatch.StartNew();
                string result = papagoTest.Translate(ocrResult.Text, BackgroundApp.LanguageCode.KOREAN, BackgroundApp.LanguageCode.ENGLISH);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine(result + " " + elapsedMs);
            }
        }

        private async void test_file_read()
        {
            var language = new Language("en");
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new Exception($"{ language.LanguageTag } is not supported in this system.");
            }
            var stream = File.OpenRead("D:\\Data\\Downloads\\Temp\\temp\\ddd.jpg");
            var decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
            var bitmap = await decoder.GetSoftwareBitmapAsync();
            var engine = OcrEngine.TryCreateFromLanguage(language);
            var ocrResult = await engine.RecognizeAsync(bitmap).AsTask();
            Console.WriteLine(ocrResult.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
