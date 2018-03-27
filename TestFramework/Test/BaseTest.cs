using NUnit.Framework;
using OpenQA.Selenium;

namespace TestFramework
{
    public class BaseTest
    {
        public Driver.BrowserType browser;
        public IWebDriver driver;

        [SetUp]
        public void startTest()
        {
            driver = Driver.Instance.getWebDriver(browser);
        }

        [TearDown]
        public void endTest()
        {
            Driver.Instance.stopBrowser();
        }
    }
}
