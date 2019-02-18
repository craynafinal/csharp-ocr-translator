using BackgroundApp;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Windows.Globalization;

namespace DesktopApp.Poco
{
    /// <summary>
    /// Collection of x, y, width and height of screen focus.
    /// </summary>
    public class Configuration
    {
        private static Configuration instance;

        private static readonly string folderName = "configuration";
        private static readonly string fileName = "configuration.txt";

        private Configuration()
        {
            if (!File.Exists(GetFullFilePath()))
            {
                /* some default settings */
                IsScreenshotAreaSet = false;
                Font = "Arial";
                FontSize = 16;
                SourceLanguage = LanguageCode.ENGLISH;
                TargetLanguage = LanguageCode.KOREAN;
                FontColor = Color.Black;
                BackgroundColor = Color.White;
                Brightness = 0;
                Contrast = 0;
                ScreenshotX = ScreenshotY = OutputX = OutputY = 0;
                ScreenshotWidth = ScreenshotHeight = OutputWidth = OutputHeight = 200;
                IsGrayscale = true;
                Save();
            }
            else
            {
                string allLines = File.ReadAllText(GetFullFilePath(), Encoding.UTF8);
                foreach (string line in allLines.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                {
                    if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] tokens = line.Split('#');

                    if (int.TryParse(tokens[1], out int n))
                    {
                        GetType().GetProperty(tokens[0]).SetValue(this, n);
                    } else if (bool.TryParse(tokens[1], out bool b))
                    {
                        GetType().GetProperty(tokens[0]).SetValue(this, b);
                    } else if (tokens[0].Contains("Color") && Enum.TryParse(tokens[1], out KnownColor color))
                    {
                        GetType().GetProperty(tokens[0]).SetValue(this, Color.FromKnownColor(color));
                    } else if (tokens[0].Contains("Language") && Enum.TryParse(tokens[1], out LanguageCode lang))
                    {
                        GetType().GetProperty(tokens[0]).SetValue(this, lang);
                    } else
                    {
                        GetType().GetProperty(tokens[0]).SetValue(this, tokens[1]);
                    }
                }
            }
        }

        /// <summary>
        /// Save the configuration to a separate text file.
        /// </summary>
        /// <returns>True if successful; false otherwise</returns>
        public bool Save()
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool result = true;
            try { 
                foreach (PropertyInfo prop in typeof(Configuration).GetProperties())
                {
                    var value = prop.GetValue(this);
                    if (prop.Name.Contains("Color") && value.GetType().Equals(typeof(Color)))
                    {
                        value = ((Color)value).Name;
                    }
                    stringBuilder.Append(prop.Name + "#" + value + "\r\n");
                }

                if (!Directory.Exists(GetFolderPath()))
                {
                    Directory.CreateDirectory(GetFolderPath());
                }

                File.WriteAllText(GetFullFilePath(), stringBuilder.ToString(), Encoding.Unicode);
                
            } catch (Exception)
            {
                result = false;
            }

            return result;
        }

        private string GetFullFilePath()
        {
            return GetFolderPath() + "\\" + fileName;
        }

        private string GetFolderPath()
        {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\" + folderName;
        }

        public Language GetSourceLanguage()
        {
            return new Language(SourceLanguage.ToLanguageCode());
        }

        public Language GetTargetLanguage()
        {
            return new Language(TargetLanguage.ToLanguageCode());
        }

        /// <summary>
        /// Get instance of configuration.
        /// </summary>
        /// <returns>Configuration instance</returns>
        public static Configuration GetInstance()
        {
            if (instance == null)
            {
                instance = new Configuration();
            }

            return instance;
        }

        /* screenshot area */
        public int ScreenshotX { get; set; }
        public int ScreenshotY { get; set; }
        public int ScreenshotWidth { get; set; }
        public int ScreenshotHeight { get; set; }
        public bool IsScreenshotAreaSet { get; set; }
        
        /* font */
        public string Font { get; set; }
        public int FontSize { get; set; }
        public Color FontColor { get; set; }
        public Color BackgroundColor { get; set; }

        /* output area */
        public int OutputX { get; set; }
        public int OutputY { get; set; }
        public int OutputWidth { get; set; }
        public int OutputHeight { get; set; }

        /* language setting */
        public LanguageCode SourceLanguage { get; set; }
        public LanguageCode TargetLanguage { get; set; }

        /* contrast setting */
        public int Brightness { get; set; }
        public int Contrast { get; set; }
        public bool IsGrayscale { get; set; }
    }
}
