using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace TestFramework
{
    class BasePage
    {
        public void navigateTo(string url)
        {
            //Driver.Instance.getWebDriver(BaseTest.).Navigate().GoToUrl(url);
            //Driver.Instance.getWebDriver(Driver.BrowserType.Chrome).Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }

    class BBCPage : BasePage
    {
        public static By search = By.CssSelector("#orb-search-q");
        public static By searchButton = By.CssSelector("#orb-search-button");
        public static By firstLink = By.CssSelector("#search-content > ol.editors-choice.results > li > article > div > h1 > a");
    }
}
