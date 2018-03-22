using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestFramework
{
    public class Driver
    {
        private IWebDriver _driver = null;
        private static readonly Lazy<Driver> lazy =
        new Lazy<Driver>(() => new Driver());
        public IWebDriver CurrentDriver { get => startDriver(); }
        public readonly string ChromeDriverLocation = @"\..\..\Resources";

        public static Driver Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        protected IWebDriver startDriver()
        {
            IWebDriver webDriver;
            webDriver = new ChromeDriver(Driver.Instance.ChromeDriverLocation);
            return webDriver;
        }

        protected void stopDriver()
        {
            if (_driver != null)
            {
                CurrentDriver.Quit();
                _driver = null;
            }
        }
    } 
}
