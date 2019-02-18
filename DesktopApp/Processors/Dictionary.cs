using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;

namespace DesktopApp.Processors
{
    /// <summary>
    /// This will force convert some strings into another.
    /// </summary>
    class Dictionary
    {
        private Dictionary<string, string> dictionary;
        private static Dictionary instance;
        private static readonly string folderName = "dictionary";
        private static readonly string fileName = "dictionary.txt";

        private Dictionary()
        {
            dictionary = new Dictionary<string, string>();

            if (!File.Exists(GetFullFilePath()))
            {
                if (!Directory.Exists(GetFolderPath()))
                {
                    Directory.CreateDirectory(GetFolderPath());
                }

                File.WriteAllText(GetFullFilePath(), "", Encoding.Unicode);
            } else
            {
                string allLines = File.ReadAllText(GetFullFilePath(), Encoding.UTF8);
                foreach (string line in allLines.Split(new string[] { "\n" }, StringSplitOptions.None))
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

        /// <summary>
        /// Return all key value pair as key#value\n format.
        /// </summary>
        /// <returns></returns>
        public string GetFullText()
        {
            return string.Join("\n", dictionary.Select(p => p.Key + "#" + p.Value));
        }

        /// <summary>
        /// Save new dictionary to memory and the file.
        /// </summary>
        /// <param name="fullText">Full text in format</param>
        /// <returns>True if successful; false otherwise</returns>
        public bool SaveNewDictionary(string fullText)
        {
            if (string.IsNullOrEmpty(fullText) || string.IsNullOrWhiteSpace(fullText))
            {
                return false;
            }

            Dictionary<string, string> newDictionary = new Dictionary<string, string>();
            bool isSuccessful = true;

            foreach (string line in fullText.Split('\n'))
            {
                try
                {
                    string[] tokens = line.Split('#');
                    newDictionary.Add(tokens[0], tokens[1]);
                } catch (Exception)
                {
                    isSuccessful = false;
                }
            }

            if (isSuccessful) {
                try {
                    if (!Directory.Exists(GetFolderPath()))
                    {
                        Directory.CreateDirectory(GetFolderPath());
                    }

                    File.WriteAllText(GetFullFilePath(), fullText, Encoding.Unicode);
                    dictionary = newDictionary;
                } catch (Exception)
                {
                    isSuccessful = false;
                }
            }

            return isSuccessful;
        }
    }
}
