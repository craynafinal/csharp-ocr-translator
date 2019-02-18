using AForge;
using AForge.Imaging.Filters;
using System.Drawing;

namespace DesktopApp.Filters
{
    /// <summary>
    /// Collection of image filters.
    /// The main reason of this class is to separate all image related functions from form classes.
    /// </summary>
    class ImageFilter
    {
        /// <summary>
        /// Convert image to grayscale.
        /// </summary>
        /// <param name="bitmap"></param>
        public static void ConvertBitmapToGrayscale(Bitmap bitmap)
        {
            AdjustBitmapSaturation(bitmap, new Range(0, 0));
        }

        /// <summary>
        /// Adjust saturation of bitmap.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="range"></param>
        public static void AdjustBitmapSaturation(Bitmap bitmap, Range range)
        {
            HSLLinear grayscaleFilter = new HSLLinear();
            grayscaleFilter.OutSaturation = range;
            grayscaleFilter.ApplyInPlace(bitmap);
        }

        /// <summary>
        /// Adjust contrast of bitmap.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="value"></param>
        public static void AdjustBitmapContrast(Bitmap bitmap, int value)
        {
            ContrastCorrection contrastFilter = new ContrastCorrection(value);
            contrastFilter.ApplyInPlace(bitmap);
        }

        /// <summary>
        /// Adjust brightness of bitmap.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="value"></param>
        public static void AdjustBitmapBrightness(Bitmap bitmap, int value)
        {
            BrightnessCorrection brightnessFilter = new BrightnessCorrection(value);
            brightnessFilter.ApplyInPlace(bitmap);
        }


    }
}
