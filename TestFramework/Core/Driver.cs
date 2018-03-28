using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace TestFramework
{
    public class Driver
    {
        private static int MAX_WEB_DRIVERS = 3;  // Max web drivers to run in parallel.
        private static int WAIT_FREE_WEB_DRIVER_COUNT = 1000;  // Each thread will sleep this time for creating a new instance of web driver.
        private readonly Dictionary<int, IWebDriver> webDrivers = new Dictionary<int, IWebDriver>();  // Key = thread ID. Value = web driver instance.

        private static Mutex syncWebDriver = new Mutex();  // To sync `webDrivers` dict.

        public static string OutputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string DriverLocation = @"..\..\Debug\drivers";
        public static string FullPathDriverLocation = Path.GetFullPath(Path.Combine(OutputDirectory, DriverLocation));

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
            IWebDriver currentWebDriver;
            int currentThreadId = Thread.CurrentThread.GetHashCode();

            while (true)  // TODO: Add MAX_RETRIES counter.
            {
                syncWebDriver.WaitOne();
                if (webDrivers.Count >= MAX_WEB_DRIVERS)
                {  // This part can be removed if --agents=MAX_WEB_DRIVERS is specified for nunit concole runner.
                    Thread.Sleep(WAIT_FREE_WEB_DRIVER_COUNT);
                    syncWebDriver.ReleaseMutex();
                }
                else
                {  // Have possibility to create new instance of web driver.
                    if (webDrivers.TryGetValue(currentThreadId, out currentWebDriver) == false)
                    {
                        currentWebDriver = StartBrowser(browser);  
                        webDrivers[currentThreadId] = currentWebDriver;
                    }  // Else the web driver exists and will be returned.
                    syncWebDriver.ReleaseMutex();
                    break;
                }
            }

            return currentWebDriver;
        }

        protected ChromeOptions ChromeBrowserOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
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
