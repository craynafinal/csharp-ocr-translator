using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DesktopApp.Processors
{
    /// <summary>
    /// This will force convert some strings into another.
    /// </summary>
    class Dictionary
    {
        private readonly Dictionary<String, String> dictionary;
        private static Dictionary instance;
        private static string folderName = "dictionary";
        private static string fileName = "dictionary.txt";

        private Dictionary()
        {
            dictionary = new Dictionary<string, string>();

            if (!File.Exists(GetFullFilePath()))
            {
                File.WriteAllText(GetFullFilePath(), "", Encoding.Unicode);
            } else
            {
                string allLines = File.ReadAllText(GetFullFilePath(), Encoding.UTF8);
                foreach (string line in allLines.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                {
                    string[] tokens = line.Split('#');
                    dictionary.Add(tokens[0].Trim(), tokens[1].Trim());
                }
            }
        }

        private string GetFullFilePath()
        {
            return GetFolderPath() + "\\" + fileName;
        }

        private string GetFolderPath()
        {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\" + folderName;
        }

        /// <summary>
        /// Get instance of dictionary
        /// </summary>
        /// <returns>Instance of dictionary</returns>
        public static Dictionary GetInstance()
        {
            if (instance == null)
            {
                instance = new Dictionary();
            }

            return instance;
        }

        /// <summary>
        /// Apply dictionary to text.
        /// </summary>
        /// <param name="text">Target text</param>
        /// <returns>Converted text</returns>
        public string Apply(string text)
        {
            if (text != null)
            {
                foreach (KeyValuePair<string, string> entry in dictionary)
                {
                    text = Regex.Replace(text, entry.Key, entry.Value, RegexOptions.IgnoreCase);
                }
            }

            return text;
        }
    }
}
