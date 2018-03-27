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
        }
    }

    class BBCPage : BasePage
    {
        public static By search = By.CssSelector("#orb-search-q");
        public static By searchButton = By.CssSelector("#orb-search-button");
        public static By firstLink = By.CssSelector("#search-content > ol.editors-choice.results > li > article > div > h1 > a");
    }
}
