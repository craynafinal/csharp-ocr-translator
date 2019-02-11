
using AForge;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Windows.Foundation;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.UI.Xaml.Media;

using System.Windows.Input;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace DesktopApp
{
    public partial class Form1 : Form
    {
        BackgroundApp.PapagoTest papagoTest = new BackgroundApp.PapagoTest();
        private GlobalKeyHook _gHook;

        public Form1()
        {
            InitializeComponent();
            _gHook = new GlobalKeyHook();
            _gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                _gHook.HookedKeys.Add(key);
            _gHook.Hook();
        }

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X && (ModifierKeys.HasFlag(Keys.Control) || ModifierKeys.HasFlag(Keys.Shift)))
            {
                var screenForm = new ScreenForm();
                screenForm.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test_bg_read();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void test_bg_read()
        {
            var language = new Language("ja");
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new Exception($"{ language.LanguageTag } is not supported in this system.");
            }

            using (MemoryStream m = new MemoryStream())
            {
                var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                //var bitmap = new Bitmap(2, 2, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
                Graphics g = Graphics.FromImage(bitmap);
                    
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

                //bitmap = Image.MakeGrayscale3(bitmap);
                //new AForge.Imaging.Filters.LevelsLinear().Apply()
                AForge.Imaging.Filters.HSLLinear grayscaleFilter = new AForge.Imaging.Filters.HSLLinear();
                grayscaleFilter.OutSaturation = new Range(0, 0);
                grayscaleFilter.ApplyInPlace(bitmap);

                AForge.Imaging.Filters.ContrastCorrection contrastFilter = new AForge.Imaging.Filters.ContrastCorrection(255);
                contrastFilter.ApplyInPlace(bitmap);

                bitmap.Save("D:\\Data\\Downloads\\Temp\\temp\\ddd2.jpg");
                bitmap.Save(m, ImageFormat.Bmp);
                
                var decoder = await BitmapDecoder.CreateAsync(m.AsRandomAccessStream());
                var softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                var engine = OcrEngine.TryCreateFromLanguage(language);
                var ocrResult = await engine.RecognizeAsync(softwareBitmap).AsTask();

                //papagoTest.Translate(ocrResult.Text.Replace("{", "").Replace("}", "").Replace(",", "").Replace(";", "").Replace(":", ""), "en", "ko");


                //TODO: url breaks because of special characters

                var watch = System.Diagnostics.Stopwatch.StartNew();
                string result = papagoTest.Translate(ocrResult.Text, BackgroundApp.LanguageCode.JAPANESE, BackgroundApp.LanguageCode.KOREAN);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine(result + " " + elapsedMs);

                g.Flush();
            }
        }

        private async void test_file_read()
        {
            var language = new Language("en");
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new Exception($"{ language.LanguageTag } is not supported in this system.");
            }
            var stream = File.OpenRead("D:\\Data\\Downloads\\Temp\\temp\\photoshoped.jpg");

            var decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());

            var bitmap = await decoder.GetSoftwareBitmapAsync();
            var engine = OcrEngine.TryCreateFromLanguage(language);
            var ocrResult = await engine.RecognizeAsync(bitmap).AsTask();
            Console.WriteLine(ocrResult.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            test_file_read();
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = null;
            using (var image = new Bitmap("D:\\Data\\Downloads\\Temp\\temp\\zzz.jpg"))
            {
                bitmap = new Bitmap(image);
                
                //bitmap = Image.MakeGrayscale3(bitmap);
                //new AForge.Imaging.Filters.LevelsLinear().Apply()
                AForge.Imaging.Filters.HSLLinear grayscaleFilter = new AForge.Imaging.Filters.HSLLinear();
                grayscaleFilter.OutSaturation = new Range(0, 0);
                grayscaleFilter.ApplyInPlace(bitmap);
                
                //-50
                AForge.Imaging.Filters.BrightnessCorrection brightnessFilter
                    = new AForge.Imaging.Filters.BrightnessCorrection(-70);
                brightnessFilter.ApplyInPlace(bitmap);

                //50
                AForge.Imaging.Filters.ContrastCorrection contrastFilter
                    = new AForge.Imaging.Filters.ContrastCorrection(70);
                contrastFilter.ApplyInPlace(bitmap);

                bitmap.Save("D:\\Data\\Downloads\\Temp\\temp\\ddd2.jpg");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var screenForm = new ScreenForm();
            screenForm.Show();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            var drawing = new Drawing();
            drawing.DrawGraphic();
        }
    }
}
