using AForge;
using AForge.Imaging.Filters;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DesktopApp.Pocos
{
    /// <summary>
    /// Collection of bitmap image data to send to ocr engine.
    /// </summary>
    class DesktopBitmapData
    {
        public Bitmap Bitmap { get; set; }
        public MemoryStream MemoryStream { get; set; }
        public Graphics Graphics { get; set; }

        /// <summary>
        /// Convert image to grayscale.
        /// </summary>
        /// <param name="bitmap"></param>
        public void ConvertBitmapToGrayscale()
        {
            AdjustBitmapSaturation(new Range(0, 0));
        }

        /// <summary>
        /// Adjust saturation of bitmap.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="range"></param>
        public void AdjustBitmapSaturation(Range range)
        {
            HSLLinear grayscaleFilter = new HSLLinear();
            grayscaleFilter.OutSaturation = range;
            grayscaleFilter.ApplyInPlace(Bitmap);
        }

        /// <summary>
        /// Adjust contrast of bitmap.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="value"></param>
        public void AdjustBitmapContrast(int value)
        {
            ContrastCorrection contrastFilter = new ContrastCorrection(value);
            contrastFilter.ApplyInPlace(Bitmap);
        }

        /// <summary>
        /// Adjust brightness of bitmap.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="value"></param>
        public void AdjustBitmapBrightness(int value)
        {
            BrightnessCorrection brightnessFilter = new BrightnessCorrection(value);
            brightnessFilter.ApplyInPlace(Bitmap);
        }

        /// <summary>
        /// Apply changes of bitmap to memory stream.
        /// </summary>
        public void ApplyBitmapToMemory()
        {
            Bitmap.Save(MemoryStream, ImageFormat.Bmp);
        }
    }
}
