using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using OpenQA.Selenium.Support.Events;

namespace TestFramework
{
    public class Driver
    {
        private static int MAX_WEB_DRIVERS = 2;  // Max web drivers to run in parallel.
        private static int GET_WEB_DRIVER_POLL_TIME = 1000;
        private static int MAX_GET_WEB_DRIVER_RETRIES = 15;
        private readonly Dictionary<int, IWebDriver> webDrivers = new Dictionary<int, IWebDriver>();  // Key = thread ID. Value = web driver instance.

        public static string OutputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string DriverLocation = @"..\..\Debug\drivers";
        public static string FullPathDriverLocation = Path.GetFullPath(Path.Combine(OutputDirectory, DriverLocation));

        #region Singleton driver realization

        private static volatile Driver _driver;
        private static object ThreadLock = new object();

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
            IWebDriver currentWebDriver = null;
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            int retries = MAX_GET_WEB_DRIVER_RETRIES;

            while (retries-- != 0)
            {
                lock(ThreadLock)
                {
                    if (webDrivers.TryGetValue(currentThreadId, out currentWebDriver) == false)
                    {
                        if (webDrivers.Count < MAX_WEB_DRIVERS)
                        {
                            currentWebDriver = StartBrowser(browser);
                            webDrivers[currentThreadId] = currentWebDriver;
                        }
                    }
                }
                
                if (currentWebDriver != null)
                {
                     //EventFiringWebDriver eventDriver = new EventFiringWebDriver(currentWebDriver);
                     //Events events = new Events();
                     //events.startEvents(eventDriver);
                     //return eventDriver;
                    
                     return currentWebDriver;
                }

                Thread.Sleep(GET_WEB_DRIVER_POLL_TIME);
            }

            throw new System.Exception("getWebDriver() timeout");
           
        }

        protected ChromeOptions ChromeBrowserOptions()
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
                case BrowserType.IE:
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
        public void stopBrowser()
        {
            int currentThreadId = Thread.CurrentThread.GetHashCode();
            lock (ThreadLock)
            {
                if (webDrivers.TryGetValue(currentThreadId, out IWebDriver currentWebDriver) == true)
                {
                    currentWebDriver.Quit();
                    webDrivers.Remove(currentThreadId);
                }
            }
        }

        ~Driver()
        {
            // Close all web drivers if any.
            IWebDriver currentWebDriver;
            foreach (KeyValuePair<int, IWebDriver> entry in webDrivers)
            {
                currentWebDriver = entry.Value;
                currentWebDriver.Quit();
            }
            _driver = null;
        }

        public enum BrowserType
        {
            Chrome,
            Firefox,
            IE
        }
    }
}
