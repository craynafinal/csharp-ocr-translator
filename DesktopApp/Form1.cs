
using AForge;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
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
            // completely broken at this moment!

            //instance = this;
            InitializeComponent();

            this.MouseDown += new MouseEventHandler(mouse_Click);
            this.MouseMove += new MouseEventHandler(mouse_Move);

            this.Location = SystemInformation.VirtualScreen.Location;
            this.Size = SystemInformation.VirtualScreen.Size;
            g = this.CreateGraphics();
        }


        public bool LeftButtonDown = false;
        public int RectangleHeight = new int();
        public int RectangleWidth = new int();
        public bool RectangleDrawn = false;

        public System.Drawing.Point ClickPoint = new System.Drawing.Point();
        public System.Drawing.Point CurrentTopLeft = new System.Drawing.Point();
        public System.Drawing.Point CurrentBottomRight = new System.Drawing.Point();
        public System.Drawing.Point DragClickRelative = new System.Drawing.Point();

        Pen EraserPen = new Pen(Color.FromArgb(255, 255, 192), 1);
        Pen MyPen = new Pen(Color.Black, 1);

        Graphics g;

        private void mouse_Click(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                LeftButtonDown = true;
                ClickPoint = new System.Drawing.Point(System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y);

                if (RectangleDrawn)
                {

                    RectangleHeight = CurrentBottomRight.Y - CurrentTopLeft.Y;
                    RectangleWidth = CurrentBottomRight.X - CurrentTopLeft.X;
                    DragClickRelative.X = Cursor.Position.X - CurrentTopLeft.X;
                    DragClickRelative.Y = Cursor.Position.Y - CurrentTopLeft.Y;

                }
            }
        }
        
        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (LeftButtonDown && !RectangleDrawn)
            {
                DrawSelection();
            }
        }

        private void DrawSelection()
        {
            this.Cursor = Cursors.Arrow;

            //Erase the previous rectangle
            g.DrawRectangle(EraserPen, CurrentTopLeft.X - this.Location.X, CurrentTopLeft.Y - this.Location.Y, CurrentBottomRight.X - CurrentTopLeft.X, CurrentBottomRight.Y - CurrentTopLeft.Y);

            //Calculate X Coordinates
            if (Cursor.Position.X < ClickPoint.X)
            {

                CurrentTopLeft.X = Cursor.Position.X;
                CurrentBottomRight.X = ClickPoint.X;

            }
            else
            {

                CurrentTopLeft.X = ClickPoint.X;
                CurrentBottomRight.X = Cursor.Position.X;

            }

            //Calculate Y Coordinates
            if (Cursor.Position.Y < ClickPoint.Y)
            {

                CurrentTopLeft.Y = Cursor.Position.Y;
                CurrentBottomRight.Y = ClickPoint.Y;

            }
            else
            {
                CurrentTopLeft.Y = ClickPoint.Y;
                CurrentBottomRight.Y = Cursor.Position.Y;
            }

            //Draw a new rectangle
            g.DrawRectangle(MyPen, CurrentTopLeft.X - this.Location.X, CurrentTopLeft.Y - this.Location.Y, CurrentBottomRight.X - CurrentTopLeft.X, CurrentBottomRight.Y - CurrentTopLeft.Y);

        }
    }
}
