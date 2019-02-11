using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;

namespace DesktopApp.Pocos
{
    /// <summary>
    /// Collection of bitmap image data to send to ocr engine.
    /// </summary>
    class DesktopBitmapData
    {
        public Bitmap Bitmap { get; set; }
        public Language Language { get; set; }
        public MemoryStream MemoryStream { get; set; }
        public Graphics Graphics { get; set; }
    }
}
