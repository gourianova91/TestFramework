using OpenQA.Selenium;

namespace TestFramework
{
    public class BBCPage : BasePage
    {
        public static By search = By.CssSelector("#orb-search-q");
        public static By searchButton = By.CssSelector("#orb-search-button");
        public static By firstLink = By.CssSelector("#search-content > ol.editors-choice.results > li > article > div > h1 > a");

        public void searchText(string text)
        {
            enterText(search, text);
            clickOnElement(searchButton);
            clickOnElement(firstLink);
        }
    }
}
