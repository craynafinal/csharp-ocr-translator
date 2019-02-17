using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Polly;
using System;
using System.Text.RegularExpressions;

namespace BackgroundApp
{
    /// <summary>
    /// Papago ui test for fetching translated text.
    /// </summary>
    public class PapagoTest
    {
        private IWebDriver _webDriver;
        private By _translatedTextArea = By.Id("txtTarget");
        private const int _waitTime = 20;
        private string previousSourceText;
        private string previousTranslatedText;

        public PapagoTest()
        {
            _webDriver = createWebDriver();
        }

        private IWebDriver createWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("window-size=1920,1080");
            options.AddArgument("headless");
            IWebDriver webDriver = new ChromeDriver(options);
            return webDriver;
        }

        /// <summary>
        /// Quit if web driver is initialized.
        /// </summary>
        public void Quit()
        {
            if (_webDriver != null)
            {
                _webDriver.Quit();
            }
        }

        /// <summary>
        /// Translate the text from source language to target language.
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <param name="sourceLanguage">Source language</param>
        /// <param name="targetLanguage">Target language</param>
        /// <returns>Translated text</returns>
        public string Translate(string text, LanguageCode sourceLanguage, LanguageCode targetLanguage)
        {
            /* return nothing if character length is greater than 2000 for now. */
            if (text.Length > 2000)
            {
                return "The input string is longer than 2000.";
            }
            
            /* if trying to translate the same text, return cached text. */
            if (text.Equals(previousSourceText))
            {
                return previousTranslatedText;
            }

            previousSourceText = text;

            _webDriver.Url = GetUrl(RemoveIllegalCharacters(text), sourceLanguage, targetLanguage);
            string result = GetTranslatedText();
            previousTranslatedText = result;
            return result;
        }
        string test;
        private string GetTranslatedText()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, _waitTime));
            string translatedText = "";

            Policy
                .Handle<NoSuchElementException>()
                .WaitAndRetry(100, retryAttempt => TimeSpan.FromSeconds(2))
                .Execute(() => {
                    wait.Until(driver => driver.FindElement(_translatedTextArea));
                    translatedText = _webDriver.FindElement(_translatedTextArea).Text;
                    test = _webDriver.FindElement(_translatedTextArea).Text;
                });
            
            return translatedText;
        }

        private string GetUrl(string text, LanguageCode sourceLanguage, LanguageCode targetLanguage)
        {
            string url = "https://papago.naver.com/?sk=[SOURCE]&tk=[TARGET]&st=[TEXT]"
                .Replace("[SOURCE]", sourceLanguage.ToLanguageCode())
                .Replace("[TARGET]", targetLanguage.ToLanguageCode())
                .Replace("[TEXT]", Uri.EscapeUriString(text));

            if (targetLanguage.Equals(LanguageCode.KOREAN))
            {
                url.Replace("&st", "&hn=0&");
            }

            return url;
        }

        private string RemoveIllegalCharacters(string text)
        {
            return new Regex("[{}^]").Replace(text, "");
        }

        static void Main(String[] args)
        {
        }

        ~PapagoTest()
        {
            this.Quit();
        }
    }
}
