using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace TestFramework
{
    class BBCPage
    {
        public static By a = By.XPath("");

        public void navigateTo(string url)
        {
            Driver.Instance.CurrentBrowser.Navigate().GoToUrl(url);
            Driver.Instance.CurrentBrowser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }
}
