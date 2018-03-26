using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    public class BaseTest
    {
        public Driver.BrowserType browser;
        public IWebDriver driver;
        public IWebElement webElement;
        public static bool enable = false;
        public static bool display = false;
        public double WAIT_IMPLICIT = 15;
        public WebDriverWait waitExplicit;


        protected static string url = "http://www.bbc.com/";
        protected static string text = "Sherlock";

        [SetUp]
        public void startTest()
        {
            driver = Driver.Instance.getWebDriver(browser);
            //Implicit wait
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(WAIT_IMPLICIT);
            //Explicit wait
            waitExplicit = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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
            BBCPage bbc = new BBCPage();
            bbc.navigateTo(url);
            //Explicit wait
            //webElement = waitExplicit.Until<IWebElement>(driver => driver.FindElement(BBCPage.search));
            enable = waitExplicit.Until(CustomExpectedConditions.IsEnabled(BBCPage.search));
            Assert.True(enable);
            display = waitExplicit.Until(CustomExpectedConditions.IsDisplayed(BBCPage.search));
            Assert.True(display);
            driver.FindElement(BBCPage.search).Clear();
            driver.FindElement(BBCPage.search).SendKeys(text);
            driver.FindElement(BBCPage.searchButton).Click();
            driver.FindElement(BBCPage.firstLink).Click();
        }
    }
}
