using DesktopApp.Processors;
using System;
using System.Windows.Forms;

namespace DesktopApp.Forms
{
    public partial class DictionaryEditor : Form
    {
        public DictionaryEditor()
        {
            InitializeComponent();
            DictionaryTextBox.Text = Dictionary.GetInstance().GetFullText();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Dictionary.GetInstance().SaveNewDictionary(DictionaryTextBox.Text))
            {
                NotificationLabel.Text = "Saved Successfully...";
            } else
            {
                NotificationLabel.Text = "Cannot save the dictionary...";
            }

            Timer timer = new Timer()
            {
                Interval = 2000,
                Enabled = true
            };

            timer.Tick += (timerSender, timerException) => {
                NotificationLabel.Text = "";
                timer.Dispose();
            };
        }

        private void DictionaryTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
