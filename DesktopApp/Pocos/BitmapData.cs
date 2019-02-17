using System.Drawing;
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
    }
}
