using DesktopApp.Poco;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System;
using BackgroundApp;

namespace DesktopApp.Forms
{
    /// <summary>
    /// Configuration editor ui.
    /// </summary>
    public partial class ConfigEditor : Form
    {
        private Configuration configuration;

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

            this.configuration = configuration;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            configuration.ScreenshotX = (int)ScreenXNumBox.Value;
            configuration.ScreenshotY = (int)ScreenYNumBox.Value;
            configuration.ScreenshotWidth = (int)ScreenWNumBox.Value;
            configuration.ScreenshotHeight = (int)ScreenHNumBox.Value;

            configuration.OutputX = (int)OutputXNumBox.Value;
            configuration.OutputY = (int)OutputYNumBox.Value;
            configuration.OutputWidth = (int)OutputWNumBox.Value;
            configuration.OutputHeight = (int)OutputHNumBox.Value;

            configuration.Font = FontDropdown.Text;
            configuration.FontSize = (int)FontSizeNumBox.Value;
            configuration.FontColor = Color.FromName(FontColorDropdown.Text);
            configuration.BackgroundColor = Color.FromName(BGColorDropdown.Text);

            Enum.TryParse(SourceLangDropdown.Text, out LanguageCode sourceLang);
            configuration.SourceLanguage = sourceLang;
            Enum.TryParse(TargetLangDropdown.Text, out LanguageCode targetLang);
            configuration.TargetLanguage = targetLang;

            if (configuration.Save())
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
    }
}
