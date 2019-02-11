using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DesktopApp
{
    class Drawing
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        public void DrawGraphic()
        {
            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            Graphics g = Graphics.FromHdc(desktopPtr);

            string drawString = "Sample Text";
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            float x = 150.0F;
            float y = 50.0F;


            SizeF size = g.MeasureString(drawString, drawFont);

            Rectangle rect = new Rectangle((int)x, (int)y, 50, 300);

            g.FillRectangle(new SolidBrush(Color.White), rect);


            StringFormat drawFormat = new StringFormat();
            g.DrawString(drawString, drawFont, drawBrush, rect);
            //g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();

            g.Dispose();
            ReleaseDC(IntPtr.Zero, desktopPtr);
        }
    }
}
