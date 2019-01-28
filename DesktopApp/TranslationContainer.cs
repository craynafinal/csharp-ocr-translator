using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp
{
    class TranslationContainer
    {
        public string sourceLanguage { get; set; }
        public string targetLanguage { get; set; }
        public string sourceText { get; set; }
        public string targetText { get; set; }
        public string errorMessage { get; set; }
    }
}
