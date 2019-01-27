using System;
using System.IO;
using System.Windows.Forms;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace DesktopApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var language = new Language("ko");
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
    }
}
