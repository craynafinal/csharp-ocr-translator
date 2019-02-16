using AForge;
using BackgroundApp;
using DesktopApp.Filters;
using DesktopApp.Forms;
using DesktopApp.Poco;
using DesktopApp.Pocos;
using DesktopApp.Processors;
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
    /// <summary>
    /// Main form.
    /// </summary>
    public partial class Main : Form
    {
        private PapagoTest papagoTest = new PapagoTest();
        private GlobalKeyHook globalKeyHook;
        private ScreenFocus screenFocusReadDesktop;
        private bool isScreenFocused = false;

        public Main()
        {
            InitializeComponent();
            SetupGlobalKeyHook();
        }

        private void SetupGlobalKeyHook()
        {
            globalKeyHook = new GlobalKeyHook();
            globalKeyHook.KeyDown += new KeyEventHandler(GlobalKeyHook_KeyDown);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                globalKeyHook.HookedKeys.Add(key);
            }

            globalKeyHook.Hook();
        }

        /// <summary>
        /// Set of rules for key down shortcuts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GlobalKeyHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X && (ModifierKeys.HasFlag(Keys.Control) || ModifierKeys.HasFlag(Keys.Shift)))
            {
                screenFocusReadDesktop = new ScreenFocus();
                OpenScreenGrabber(screenFocusReadDesktop);
            }

        }

        private DesktopBitmapData ReadFromDesktop()
        {
            if (screenFocusReadDesktop == null)
            {
                throw new Exception("Initialize screen focus first!");
            }

            

            MemoryStream memoryStream = new MemoryStream();

            var bitmap = new Bitmap(screenFocusReadDesktop.Width, screenFocusReadDesktop.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(screenFocusReadDesktop.X, screenFocusReadDesktop.Y, 0, 0, 
                new Size(screenFocusReadDesktop.Width, screenFocusReadDesktop.Height), CopyPixelOperation.SourceCopy);

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

        private async void TranslateBitmapText(DesktopBitmapData desktopBitmapData)
        {
            /* need to be here? */
            var language = new Language("en");
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new Exception($"{ language.LanguageTag } is not supported in this system.");
            }

            var decoder = await BitmapDecoder.CreateAsync(desktopBitmapData.MemoryStream.AsRandomAccessStream());
            var softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            var engine = OcrEngine.TryCreateFromLanguage(new Language("en"));
            var ocrResult = await engine.RecognizeAsync(softwareBitmap).AsTask();

            /* need to be dynamic */
            string result = papagoTest.Translate(Dictionary.GetInstance().Apply(ocrResult.Text), LanguageCode.ENGLISH, LanguageCode.KOREAN);
            desktopBitmapData.Graphics.Flush();

            // TOOD: debugging purpose
            Console.WriteLine(ocrResult.Text + "\n" + result);
        }

        private void DrawText()
        {
            var drawing = new Drawing();
            drawing.DrawGraphic();
        }

        private void OpenScreenGrabber(ScreenFocus screenFocus)
        {
            var screenForm = new ScreenGrabber(screenFocus);
            screenForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TranslateBitmapText(ReadFromDesktop());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dictionaryEditor = new DictionaryEditor();
            dictionaryEditor.Show();
        }


        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
