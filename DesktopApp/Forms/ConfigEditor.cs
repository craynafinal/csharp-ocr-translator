using DesktopApp.Poco;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System;
using BackgroundApp;
using DesktopApp.Processors;
using DesktopApp.Pocos;

namespace DesktopApp.Forms
{
    /// <summary>
    /// Configuration editor ui.
    /// </summary>
    public partial class ConfigEditor : Form
    {
        private Configuration configuration;
        private DesktopBitmapData desktopBitmapData;

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

            BrightnessTrackBar.Value = configuration.Brightness;
            ContrastTrackBar.Value = configuration.Contrast;
            GrayscaleCheckBox.Checked = configuration.IsGrayscale;
            UpdateBrightnessLabel(BrightnessTrackBar.Value);
            UpdateContrastLabel(ContrastTrackBar.Value);

            desktopBitmapData = ImageGrabber.ReadFromDesktop(configuration);
            if (configuration.IsGrayscale)
            {
                desktopBitmapData.ConvertBitmapToGrayscale();
            }
            ImagePreview.Image = desktopBitmapData.Bitmap;
            ImagePreview.Invalidate();

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

            configuration.Brightness = BrightnessTrackBar.Value;
            configuration.Contrast = ContrastTrackBar.Value;
            configuration.IsGrayscale = GrayscaleCheckBox.Checked;

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

        private void BrightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            if (configuration != null)
            {
                UpdateBrightnessLabel(BrightnessTrackBar.Value);
                desktopBitmapData = ImageGrabber.ReadFromDesktop(configuration);

                if (GrayscaleCheckBox.Checked)
                {
                    desktopBitmapData.ConvertBitmapToGrayscale();
                }

                desktopBitmapData.AdjustBitmapBrightness(BrightnessTrackBar.Value);
                ImagePreview.Image = desktopBitmapData.Bitmap;
                ImagePreview.Invalidate();
            }
        }

        private void UpdateBrightnessLabel(int value)
        {
            BrightnessLabel.Text = "Brightness (" + value + ")";
        }

        private void ContrastTrackBar_Scroll(object sender, EventArgs e)
        {
            if (configuration != null)
            {
                UpdateContrastLabel(ContrastTrackBar.Value);
                desktopBitmapData = ImageGrabber.ReadFromDesktop(configuration);

                if (GrayscaleCheckBox.Checked)
                {
                    desktopBitmapData.ConvertBitmapToGrayscale();
                }

                desktopBitmapData.AdjustBitmapContrast(ContrastTrackBar.Value);
                ImagePreview.Image = desktopBitmapData.Bitmap;
                ImagePreview.Invalidate();
            }
        }

        private void UpdateContrastLabel(int value)
        {
            ContrastLabel.Text = "Contrast (" + value + ")";
        }

        private void GrayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (configuration != null)
            {
                desktopBitmapData = ImageGrabber.ReadFromDesktop(configuration);

                if (GrayscaleCheckBox.Checked)
                {
                    desktopBitmapData.ConvertBitmapToGrayscale();
                }

                desktopBitmapData.AdjustBitmapBrightness(BrightnessTrackBar.Value);
                desktopBitmapData.AdjustBitmapContrast(ContrastTrackBar.Value);
                ImagePreview.Image = desktopBitmapData.Bitmap;
                ImagePreview.Invalidate();
            }
        }
    }
}
