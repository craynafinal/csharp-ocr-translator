using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace BackgroundApp
{
    class PapagoTest
    {
        private IWebDriver webDriver;

        public PapagoTest()
        {
            this.webDriver = getWebDriver();
        }

        /// <summary>
        /// Get webdriver instance.
        /// </summary>
        /// <returns>
        /// Web driver instance.
        /// </returns>
        private IWebDriver getWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("window-size=1920,1080");
            IWebDriver webDriver = new ChromeDriver(options);
            return webDriver;
        }

        /// <summary>
        /// Quit if web driver is initialized.
        /// </summary>
        public void Quit()
        {
            if (webDriver != null)
            {
                webDriver.Quit();
            }
        }

        /// <summary>
        /// Translate the text from source language to target language.
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <param name="sourceLanguage">Source language</param>
        /// <param name="targetLanguage">Target language</param>
        /// <returns>Translated text</returns>
        public string Translate(string text, string sourceLanguage, string targetLanguage)
        {
            webDriver.Url = "https://papago.naver.com/?sk=[SOURCE]&tk=[TARGET]&st=[TEXT]"
                .Replace("[SOURCE]", sourceLanguage)
                .Replace("[TARGET]", targetLanguage)
                .Replace("[TEXT]", text);

            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 20));
            wait.Until(driver => driver.FindElement(By.Id("txtTarget")));
            IWebElement webElement = webDriver.FindElement(By.Id("txtTarget"));
            return webElement.Text;
        }

        static void Main(string[] args)
        {
            PapagoTest papagoTest = new PapagoTest();
            Console.WriteLine(papagoTest.Translate("hello", "en", "ko"));
            papagoTest.Quit();
        }
    }
}
