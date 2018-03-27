using OpenQA.Selenium;

namespace TestFramework
{
    class BBCPage : BasePage
    {
        public static By search = By.CssSelector("#orb-search-q");
        public static By searchButton = By.CssSelector("#orb-search-button");
        public static By firstLink = By.CssSelector("#search-content > ol.editors-choice.results > li > article > div > h1 > a");

        public void searchText(string text)
        {
            //Check isEnabled and isDisplayed Methods
            //Waiter wait = new Waiter();
            //Assert.True(wait.isEnabled(By.CssSelector("#search-q")));
            //Assert.True(wait.isEnabled(BBCPage.search));
            //Assert.True(wait.isDisplayed(BBCPage.search));
            driver.FindElement(search).Clear();
            driver.FindElement(search).SendKeys(text);
            driver.FindElement(searchButton).Click();
            driver.FindElement(firstLink).Click();
        }
    }
}
