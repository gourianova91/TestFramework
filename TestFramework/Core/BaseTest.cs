using NUnit.Framework;
using OpenQA.Selenium;

namespace TestFramework
{
    [TestFixture]
    public class BaseTest
    {
        public Driver.BrowserType browser;

        [SetUp]
        public void startTest()
        {
            Driver.Instance.getWebDriver(browser);
        }

        [TearDown]
        public void endTest()
        {
            Driver.Instance.stopBrowser();
        }
    }
}
