using DesktopApp.Poco;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopApp
{
    /// <summary>
    /// Screen that grabs area to measure sizes.
    /// </summary>
    public partial class ScreenGrabber : Form
    {
        private Graphics formGraphics;
        private bool isDown = false;
        private int initialX, initialY, areaX, areaY, areaWidth, areaHeight;
        private Configuration configuration;

        private void ScreenGrabberMouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            initialX = e.X;
            initialY = e.Y;
        }

        private void ScreenGrabberMouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true)
            {
                this.Refresh();
                Pen drwaPen = new Pen(Color.Red, 3);
                int width = e.X - initialX, height = e.Y - initialY;

                areaX = Math.Min(e.X, initialX);
                areaY = Math.Min(e.Y, initialY);
                areaWidth = Math.Abs(e.X - initialX);
                areaHeight = Math.Abs(e.Y - initialY);

                Rectangle rect = new Rectangle(areaX, areaY, areaWidth, areaHeight);

                formGraphics = this.CreateGraphics();
                formGraphics.DrawRectangle(drwaPen, rect);
            }
        }

        private void ScreenGrabberMouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            configuration.ScreenshotX = areaX;
            configuration.ScreenshotY = areaY;
            configuration.ScreenshotWidth = areaWidth;
            configuration.ScreenshotHeight = areaHeight;
            configuration.IsScreenshotAreaSet = true;
            this.Close();
        }

        /// <summary>
        /// Initialize screen grabber and assigns mouse event actions.
        /// </summary>
        public ScreenGrabber(Configuration configuration)
        {
            InitializeComponent();
            this.configuration = configuration;
            this.DoubleBuffered = true;
            this.Location = SystemInformation.VirtualScreen.Location;
            this.Size = SystemInformation.VirtualScreen.Size;
            this.MouseDown += new MouseEventHandler(this.ScreenGrabberMouseDown);
            this.MouseMove += new MouseEventHandler(this.ScreenGrabberMouseMove);
            this.MouseUp += new MouseEventHandler(this.ScreenGrabberMouseUp);
        }
    }
}
