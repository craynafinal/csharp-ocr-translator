using DesktopApp.Poco;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Windows.Foundation;

namespace DesktopApp.Forms
{
    public partial class Output : Form
    {
        private Configuration configuration;

        public Output(Configuration configuration)
        {
            InitializeComponent(configuration.OutputX, configuration.OutputY, configuration.OutputWidth, configuration.OutputHeight);
            //Top = configuration.OutputX;
            //Left = configuration.OutputY;
            //Width = configuration.OutputWidth;
            //Height = configuration.OutputHeight;
            //OutputTextBox.Width = Width;
            //OutputTextBox.Height = Height;
            //OutputTextBox.Top = 0;
            //OutputTextBox.Left = 0;

            this.configuration = configuration;
        }

        private void Output_Resize(object sender, EventArgs e)
        {
            if (configuration != null)
            {
                configuration.OutputWidth = Width;
                configuration.OutputHeight = Height;
                OutputTextBox.Width = Width;
                OutputTextBox.Height = Height;
            }
        }

        private void Output_Move(object sender, EventArgs e)
        {
            if (configuration != null)
            {
                configuration.OutputX = Location.X;
                configuration.OutputY = Location.Y;
            }
        }

        private void Output_Closing(object sender, FormClosingEventArgs e)
        {
            configuration.OutputWidth = Width;
            configuration.OutputHeight = Height;
            configuration.OutputX = Location.X;
            configuration.OutputY = Location.Y;
        }

        /// <summary>
        /// Set output textbox.
        /// </summary>
        /// <param name="fullText"></param>
        public void SetTextBox(string fullText)
        {
            Invoke(new Action(() =>
            {
                OutputTextBox.Font = new Font(configuration.Font, configuration.FontSize);
                OutputTextBox.ForeColor = configuration.FontColor;
                OutputTextBox.BackColor = configuration.BackgroundColor;
                OutputTextBox.Text = fullText;
                OutputTextBox.Invalidate();
            }));
        }
    }
}
