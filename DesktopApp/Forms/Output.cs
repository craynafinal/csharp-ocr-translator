using DesktopApp.Poco;
using System;
using System.Windows.Forms;

namespace DesktopApp.Forms
{
    public partial class Output : Form
    {
        public Output(Configuration configuration)
        {
            InitializeComponent();
            Top = configuration.OutputX;
            Left = configuration.OutputY;
            Width = configuration.OutputWidth;
            Height = configuration.OutputHeight;
        }

        public void SetTextBox(string fullText)
        {
            Invoke(new Action(() =>
            {
                OutputTextBox.Text = fullText;
            }));
        }
    }
}
