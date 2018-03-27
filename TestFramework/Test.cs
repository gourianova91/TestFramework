using System;
using NUnit.Framework;
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

            //Check isEnabled and isDisplayed Methods
            //Waiter wait = new Waiter();
            //Assert.True(wait.isEnabled(By.CssSelector("#search-q")));
            //Assert.True(wait.isEnabled(BBCPage.search));
            //Assert.True(wait.isDisplayed(BBCPage.search));

            driver.FindElement(BBCPage.search).Clear();
            driver.FindElement(BBCPage.search).SendKeys(text);
            driver.FindElement(BBCPage.searchButton).Click();
            driver.FindElement(BBCPage.firstLink).Click();
        }
    }
}
