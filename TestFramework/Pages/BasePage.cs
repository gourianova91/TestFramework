using OpenQA.Selenium;
using System.Collections.Generic;

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
        }

        public bool listsCompare(IList<IWebElement> listLondon, IList<IWebElement> listParis)
        {
            return false;
        }
    }
}
