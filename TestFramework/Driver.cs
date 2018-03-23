using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Reflection;

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

        public static string OutputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string DriverLocation = @"..\..\Debug\drivers";
        public static string FullPathDriverLocation = Path.GetFullPath(Path.Combine(OutputDirectory, DriverLocation));

        protected ChromeOptions ChromeBrowserOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            return options;
        }
        

        public IWebDriver GetDriver(BrowserType browser = BrowserType.Chrome)
        {
            lock (ThreadLock)
            {
                if (_driver == null)
                {
                    _driver = startBrowser(browser);
                }
                return _driver;
            }

        }

        private IWebDriver startBrowser(BrowserType browser = BrowserType.Chrome)
        {
            IWebDriver driver;
            switch (browser)
            {
                case BrowserType.Chrome:
                    {
                        driver = new ChromeDriver(FullPathDriverLocation, ChromeBrowserOptions());
                        break;
                    }
                case BrowserType.Firefox:
                    {
                        FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(FullPathDriverLocation);
                        service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                        driver = new FirefoxDriver(service);
                        break;
                    }
                case BrowserType.IE:
                    {
                        driver = new InternetExplorerDriver(FullPathDriverLocation);
                        driver.Manage().Window.Maximize();
                        break;
                    }
                default:
                    {
                        driver = new ChromeDriver(FullPathDriverLocation, ChromeBrowserOptions());
                        break;
                    }
            }
            return driver;
        }

        public void stopBrowser()
        {
            if (_driver != null)
            {
                CurrentBrowser.Close();
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
