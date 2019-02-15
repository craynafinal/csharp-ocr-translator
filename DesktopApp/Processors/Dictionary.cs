using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DesktopApp.Processors
{
    /// <summary>
    /// This will force convert some strings into another.
    /// </summary>
    class Dictionary
    {
        private readonly Dictionary<String, String> dictionary;
        private static Dictionary instance;

        private Dictionary()
        {
            dictionary = new Dictionary<string, string>();
            
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
