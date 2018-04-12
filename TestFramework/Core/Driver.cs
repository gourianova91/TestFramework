using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TestFramework.Core
{
    public class Driver
    {
        private const int MaxWebDrivers = 2;  // Max web drivers to run in parallel.
        private const int GetWebDriverPollTime = 1000;
        private const int MaxGetWebDriverRetries = 15;
        private readonly Dictionary<int, IWebDriver> _webDrivers = new Dictionary<int, IWebDriver>();  // Key = thread ID. Value = web driver instance.

        private static readonly string OutputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string DriverLocation = @"..\..\Debug\drivers";
        private static readonly string FullPathDriverLocation = Path.GetFullPath(Path.Combine(OutputDirectory, DriverLocation));

        #region Singleton driver realization

        private static volatile Driver _driver;
        private static readonly object ThreadLock = new object();

        private Driver() { }

        public static Driver Instance
        {
            get
            {
                if (_driver == null)
                {
                    lock (ThreadLock)
                    {
                        if (_driver == null)
                        {
                            _driver = new Driver();
                        }
                    }
                }
                return _driver;
            }
        }

        #endregion

        /*
         * Return a new web driver for current thread or an existing one if it was previously created.
         */
        public IWebDriver getWebDriver(BrowserType browser = BrowserType.Chrome)
        {
            int retries = MaxGetWebDriverRetries;

            while (retries-- != 0)
            {
                IWebDriver currentWebDriver = null;
                lock(ThreadLock)
                {
                    int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                    if (_webDrivers.TryGetValue(currentThreadId, out currentWebDriver) == false)
                    {
                        if (_webDrivers.Count < MaxWebDrivers)
                        {
                            currentWebDriver = StartBrowser(browser);
                            _webDrivers[currentThreadId] = currentWebDriver;
                        }
                    }
                }
                
                if (currentWebDriver != null)
                {
                    //EventFiringWebDriver eventDriver = new EventFiringWebDriver(currentWebDriver);
                    //Events events = new Events();
                    //events.StartEvents(eventDriver);
                    //return eventDriver;

                    return currentWebDriver;
                }

                Thread.Sleep(GetWebDriverPollTime);
            }

            throw new System.Exception("getWebDriver() timeout");
           
        }

        private ChromeOptions ChromeBrowserOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("ignore-certifcate-errors");
            options.AddArgument("test-type");
            options.AddArgument("disable-infobars");
            options.AddArgument("start-maximized");
            options.AddArgument("enable-automation");
            options.AddArgument("--js-flags=--expose-gc");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-default-apps");
            return options;
        }
       
        private IWebDriver StartBrowser(BrowserType browser = BrowserType.Chrome)
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
                        driver.Manage().Window.Maximize();
                        break;
                    }
                case BrowserType.Ie:
                    {
                        var service = InternetExplorerDriverService.CreateDefaultService(FullPathDriverLocation);
                        // properties on the service can be used to e.g. hide the command prompt
                        var options = new InternetExplorerOptions
                        {
                            IgnoreZoomLevel = true
                        };
                        driver = new InternetExplorerDriver(service, options);
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

        /*
        * Close web driver for current thread.
        */
        public void StopBrowser()
        {
            int currentThreadId = Thread.CurrentThread.GetHashCode();
            lock (ThreadLock)
            {
                if (_webDrivers.TryGetValue(currentThreadId, out IWebDriver currentWebDriver) == true)
                {
                    currentWebDriver.Quit();
                    _webDrivers.Remove(currentThreadId);
                }
            }
        }

        ~Driver()
        {
            // Close all web drivers if any.
            foreach (KeyValuePair<int, IWebDriver> entry in _webDrivers)
            {
                var currentWebDriver = entry.Value;
                currentWebDriver.Quit();
            }
            _driver = null;
        }

        public enum BrowserType
        {
            Chrome,
            Firefox,
            Ie
        }
    }
}
