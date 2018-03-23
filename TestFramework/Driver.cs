using System;

namespace TestFramework.Core.WebDriver
{
    public class WebDriver
    {
        private static readonly Lazy<WebDriver> lazy =
        new Lazy<WebDriver>(() => GetDriver());

        public static WebDriver Driver
        {
            get
            {
                return lazy.Value;
            }
        }

        private static WebDriver GetDriver()
        {
            return lazy.Value;
        }

        public static void StartBrowser(BrowserTypes browserType = BrowserTypes.Chrome, int defaultTimeOut = 30)
        {
            switch(browserType)
            {
                case BrowserTypes.Chrome:
                    WebDriver.GetDriver();
                    break;
                case BrowserTypes.Firefox:
                    WebDriver.GetDriver();
                    break;
                default:
                    break;
            }
        }
    }
    public enum BrowserTypes
    {
        Firefox,
        Chrome
    }
}
