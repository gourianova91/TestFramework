using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace TestFramework
{
    class BasePage
    {
        protected IWebDriver driver;
        public BasePage()
        {
            driver = Driver.Instance.getWebDriver();
        }
        public void navigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }

    class BBCPage : BasePage
    {
        public static By search = By.CssSelector("#orb-search-q");
        public static By searchButton = By.CssSelector("#orb-search-button");
        public static By firstLink = By.CssSelector("#search-content > ol.editors-choice.results > li > article > div > h1 > a");
    }
}
