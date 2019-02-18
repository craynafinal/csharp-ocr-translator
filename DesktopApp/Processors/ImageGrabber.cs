using DesktopApp.Poco;
using DesktopApp.Pocos;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DesktopApp.Processors
{
    /// <summary>
    /// Grabbing image from desktop area.
    /// </summary>
    class ImageGrabber
    {
        /// <summary>
        /// Read image from desktop.
        /// </summary>
        /// <param name="configuration">Configuration properties</param>
        /// <returns>Bitmap data</returns>
        public static DesktopBitmapData ReadFromDesktop(Configuration configuration)
        {
            MemoryStream memoryStream = new MemoryStream();

            var bitmap = new Bitmap(configuration.ScreenshotWidth, configuration.ScreenshotHeight, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            //Drawing.GetInstance().HideGraphic();

            graphics.CopyFromScreen(configuration.ScreenshotX, configuration.ScreenshotY, 0, 0,
                new Size(configuration.ScreenshotWidth, configuration.ScreenshotHeight), CopyPixelOperation.SourceCopy);

            //Drawing.GetInstance().Redraw(configuration);

            //ImageFilter.ConvertBitmapToGrayscale(bitmap);
            //ImageFilter.AdjustBitmapBrightness(bitmap, -50);
            //ImageFilter.AdjustBitmapContrast(bitmap, 50);

            bitmap.Save(memoryStream, ImageFormat.Bmp);

            DesktopBitmapData bitmapData = new DesktopBitmapData();
            bitmapData.Bitmap = bitmap;
            bitmapData.Graphics = graphics;
            bitmapData.MemoryStream = memoryStream;

            return bitmapData;
        }
    }
}
