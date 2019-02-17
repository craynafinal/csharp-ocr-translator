using DesktopApp.Poco;
using System;
using System.Windows.Forms;

namespace DesktopApp.Forms
{
    public partial class Output : Form
    {
        private Configuration configuration;

        public Output(Configuration configuration)
        {
            InitializeComponent();
            Top = configuration.OutputX;
            Left = configuration.OutputY;
            Width = configuration.OutputWidth;
            Height = configuration.OutputHeight;

            this.configuration = configuration;
        }

        private void Output_Resize(object sender, EventArgs e)
        {
            if (configuration != null)
            {
                configuration.OutputWidth = Width;
                configuration.OutputHeight = Height;
            }
        }

        private void Output_Move(object sender, EventArgs e)
        {
            if (configuration != null)
            {
                configuration.OutputX = Top;
                configuration.OutputY = Left;
            }
        }

        /// <summary>
        /// Set output textbox.
        /// </summary>
        /// <param name="fullText"></param>
        public void SetTextBox(string fullText)
        {
            Invoke(new Action(() =>
            {
                OutputTextBox.Text = fullText;
            }));
        }
    }
}
