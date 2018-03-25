using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium;

namespace TestFramework
{
    public class BaseTest
    {
        public Driver.BrowserType browser;
        public IWebDriver driver;

        protected static string url = "http://www.bbc.com/";
        protected static string text = "Sherlock";

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

    [Parallelizable]
    [TestFixture(Driver.BrowserType.Chrome)]
    [TestFixture(Driver.BrowserType.Firefox)]
    //[TestFixture(Driver.BrowserType.IE)]
    public class Test : BaseTest
    {
        public Test(Driver.BrowserType browser)
        {
            this.browser = browser;
        }

        [Test]
        public void test()
        {
            //BBCPage bbc = new BBCPage();
            //bbc.navigateToChrome(url);
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(BBCPage.search).Clear();
            driver.FindElement(BBCPage.search).SendKeys(text);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(BBCPage.searchButton).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(BBCPage.firstLink).Click();
        }
    }
}
