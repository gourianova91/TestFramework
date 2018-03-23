using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TestFramework
{
    public class Driver
    {
        private static readonly Lazy<Driver> lazy =
        new Lazy<Driver>(() => new Driver());

        public static Driver Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        private IWebDriver _driver = null;
        public IWebDriver CurrentBrowser { get => GetDriver(); }
        private static readonly object ThreadLock = new object();

        //public readonly string ChromeDriverLocation = @"\..\..\Resources";

        public IWebDriver GetDriver()
        {
            lock (ThreadLock)
            {
                if (_driver == null)
                {
                    _driver = startBrowser();
                }
                return _driver;
            }

        }

        // Converted to private method because it can't set `_driver`
        // which is used to close WebDriver (see stopBrowser).
        private IWebDriver startBrowser(BrowserType browser = BrowserType.Chrome)
        {
            IWebDriver driver;
            switch (browser)
            {
                case BrowserType.Chrome:
                    {
                        driver = new ChromeDriver();
                        break;
                    }
                case BrowserType.Firefox:
                    {
                        driver = new FirefoxDriver();
                        break;
                    }
                case BrowserType.IE:
                    {
                        driver = new InternetExplorerDriver();
                        break;
                    }
                default:
                    {
                        driver = new ChromeDriver();
                        break;
                    }
            }
            return driver;
        }

        public void stopBrowser()
        {
            if (_driver != null)
            {
                CurrentBrowser.Quit();
                _driver = null;
            }
        }

        public enum BrowserType
        {
            Chrome,
            Firefox,
            IE
        }
    } 
}
