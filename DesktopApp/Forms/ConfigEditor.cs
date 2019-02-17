using DesktopApp.Poco;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System;
using BackgroundApp;

namespace DesktopApp.Forms
{
    public partial class ConfigEditor : Form
    {
        public ConfigEditor(Configuration configuration)
        {
            InitializeComponent();
            ScreenXNumBox.Value = configuration.ScreenshotX;
            ScreenYNumBox.Value = configuration.ScreenshotY;
            ScreenWNumBox.Value = configuration.ScreenshotWidth;
            ScreenHNumBox.Value = configuration.ScreenshotHeight;
            OutputXNumBox.Value = configuration.OutputX;
            OutputYNumBox.Value = configuration.OutputY;
            OutputWNumBox.Value = configuration.OutputWidth;
            OutputHNumBox.Value = configuration.OutputHeight;

            FontDropdown.Items.AddRange(FontFamily.Families.Select(f => f.Name).ToArray<string>());
            FontDropdown.Text = configuration.Font;
            FontSizeNumBox.Value = configuration.FontSize;
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                FontColorDropdown.Items.Add(Color.FromKnownColor(color));
                BGColorDropdown.Items.Add(Color.FromKnownColor(color));
            }
            FontColorDropdown.Text = configuration.FontColor.Name;
            BGColorDropdown.Text = configuration.BackgroundColor.Name;

            foreach (LanguageCode languageCode in Enum.GetValues(typeof(LanguageCode))) {
                SourceLangDropdown.Items.Add(languageCode);
                TargetLangDropdown.Items.Add(languageCode);
            }
            SourceLangDropdown.Text = configuration.SourceLanguage.ToString();
            TargetLangDropdown.Text = configuration.TargetLanguage.ToString();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }
    }
}
