using DesktopApp.Poco;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DesktopApp
{
    class Drawing
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        private Graphics graphic;
        private static Drawing instance;
        private string previousText;

        public static Drawing GetInstance()
        {
            if (instance == null)
            {
                instance = new Drawing();
            }

            return instance;
        }

        private Drawing()
        {
        }

        public void Redraw(Configuration configuration)
        {
            if (previousText != null)
            {
                DrawGraphic(previousText, configuration);
            }
        }

        public void HideGraphic()
        {
            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            graphic = Graphics.FromHdc(desktopPtr);

            graphic.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(0, 0, 1, 1));
            graphic.DrawString("", new Font("Arial", 1), new SolidBrush(Color.Transparent), new Rectangle(0, 0, 1, 1));
            graphic.Dispose();

            ReleaseDC(IntPtr.Zero, desktopPtr);
        }

        public void DrawGraphic(string text, Configuration configuration)
        {
            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            graphic = Graphics.FromHdc(desktopPtr);

            string drawString = text;
            Font drawFont = new Font(configuration.Font, configuration.FontSize);
            SolidBrush drawBrush = new SolidBrush(configuration.FontColor);

            Rectangle rect = new Rectangle(configuration.OutputX, configuration.OutputY, configuration.OutputWidth, configuration.OutputHeight);
            graphic.FillRectangle(new SolidBrush(configuration.BackgroundColor), rect);

            StringFormat drawFormat = new StringFormat();
            graphic.DrawString(drawString, drawFont, drawBrush, rect);
            drawFont.Dispose();
            drawBrush.Dispose();

            graphic.Dispose();
            ReleaseDC(IntPtr.Zero, desktopPtr);
            previousText = text;
        }
    }
}
