using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class ScreenForm : Form
    {
        System.Drawing.Graphics formGraphics;
        bool isDown = false;
        int initialX;
        int initialY;

        int areaX, areaY, areaWidth, areaHeight;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            initialX = e.X;
            initialY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true)
            {
                this.Refresh();
                Pen drwaPen = new Pen(Color.Red, 3);
                int width = e.X - initialX, height = e.Y - initialY;
                //if (Math.Sign (width) == -1) width = width 
                //Rectangle rect = new Rectangle(initialPt.X, initialPt.Y, Cursor.Position.X - initialPt.X, Cursor.Position.Y - initialPt.Y); 

                areaX = Math.Min(e.X, initialX);
                areaY = Math.Min(e.Y, initialY);
                areaWidth = Math.Abs(e.X - initialX);
                areaHeight = Math.Abs(e.Y - initialY);

                Rectangle rect = new Rectangle(areaX, areaY, areaWidth, areaHeight);

                formGraphics = this.CreateGraphics();
                formGraphics.DrawRectangle(drwaPen, rect);
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            Console.WriteLine(areaX + " " + areaY + " " + areaWidth + " " + areaHeight);
            this.Close();
        }

        public ScreenForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Location = SystemInformation.VirtualScreen.Location;
            this.Size = SystemInformation.VirtualScreen.Size;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
        }
    }
}
