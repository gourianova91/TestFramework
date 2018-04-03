using NUnit.Framework;

namespace TestFramework
{
    public class BaseTest
    {
        protected Driver.BrowserType browser;

        public BaseTest(Driver.BrowserType browser)
        {
            this.browser = browser;
            //Driver.Instance.getWebDriver(browser);
        }

        [SetUp]
        public void startTest()
        {
            Driver.Instance.getWebDriver(browser);
        }

        [TearDown]
        public void endTest()
        {
            System.Threading.Thread.Sleep(1000);
            Driver.Instance.stopBrowser();
        }
    }
}
