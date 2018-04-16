using OpenQA.Selenium;
using TestFramework.Core;

namespace TestFramework.Pages
{
    public class BBCPage : BasePage
    {
        private static readonly By Search = By.CssSelector("#orb-search-q");
        private static readonly By SearchButton = By.CssSelector("#orb-search-button");
        private static readonly By FirstLink = By.CssSelector("#search-content > ol.editors-choice.results > li > article > div > h1 > a");

        public void SearchText(string text)
        {
            EnterText(Search, text);
            ClickOnElement(SearchButton);
            ClickOnElement(FirstLink);
        }
    }
}
