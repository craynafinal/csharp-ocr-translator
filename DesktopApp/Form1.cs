
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

namespace DesktopApp
{
    public partial class Form1 : Form
    {
        BackgroundApp.PapagoTest papagoTest = new BackgroundApp.PapagoTest();

        ~Form1()
        {
            //papagoTest.Dispose();
        }

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            test_bg_read();
        }

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

        }


        System.Drawing.Graphics formGraphics;
        bool isDown = false;
        int initialX;
        int initialY;

        int areaX, areaY, areaWidth, areaHeight;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            initialX = e.X;
            initialY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true)
            {
                this.Refresh();
                Pen drwaPen = new Pen(Color.Navy, 1);
                int width = e.X - initialX, height = e.Y - initialY;
                //if (Math.Sign (width) == -1) width = width 
                //Rectangle rect = new Rectangle(initialPt.X, initialPt.Y, Cursor.Position.X - initialPt.X, Cursor.Position.Y - initialPt.Y); 

                areaX = Math.Min(e.X, initialX);
                areaY = Math.Min(e.Y, initialY);
                areaWidth = Math.Abs(e.X - initialX);
                areaHeight = Math.Abs(e.Y - initialY);

                Rectangle rect = new Rectangle(areaX, areaY, areaWidth, areaHeight);

                formGraphics = this.CreateGraphics();
                formGraphics.DrawRectangle(drwaPen, rect);
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            Console.WriteLine(areaX + " " + areaY + " " + areaWidth + " " + areaHeight);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
        }
    }
}
