using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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

        public PapagoTest()
        {
            this._webDriver = createWebDriver();
        }

        private IWebDriver createWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("window-size=1920,1080");
            //options.AddArgument("headless");
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
            _webDriver.Navigate().GoToUrl(GetUrl(RemoveIllegalCharacters(text), sourceLanguage, targetLanguage));
            return GetTranslatedText();
        }

        private string GetTranslatedText()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, _waitTime));
            wait.Until(driver => driver.FindElement(_translatedTextArea));
            IWebElement webElement = _webDriver.FindElement(_translatedTextArea);
            return webElement.Text;
        }

        private string GetUrl(string text, LanguageCode sourceLanguage, LanguageCode targetLanguage)
        {
            string url = "https://papago.naver.com/?sk=[SOURCE]&tk=[TARGET]&st=[TEXT]"
                .Replace("[SOURCE]", sourceLanguage.ToLanguageCode())
                .Replace("[TARGET]", targetLanguage.ToLanguageCode())
                .Replace("[TEXT]", text);

            if (targetLanguage.Equals(LanguageCode.KOREAN))
            {
                url.Replace("&st", "&hn=0&");
            }

            return url;
        }

        private string RemoveIllegalCharacters(string text)
        {
            return new Regex("[{}]").Replace(text, "");
        }

        static void Main(String[] args)
        {

        }
    }
}
