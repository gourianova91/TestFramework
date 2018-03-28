using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;

namespace TestFramework
{
    class YandexPage : BasePage
    {
        public static By geolocation = By.CssSelector("div.col.headline__item.headline__leftcorner > a");
        public static By city = By.CssSelector("#city__front-input");
        public static By cityList = By.CssSelector("ul.popup__items.input__popup-items");
        public static By more = By.CssSelector("a.home-link.home-link_blue_yes.home-tabs__link.home-tabs__more-switcher");
        public static By moreList = By.CssSelector("div.home-tabs__more");
        public static By checkbox = By.CssSelector("input.checkbox__control");

        public void changeGeolocation(string cityName)
        {
            clickOnElement(geolocation);
            uncheckCheckbox(checkbox);
            enterText(city, cityName);
            waitForAutocompleteJquery(cityList, city);
        }

        public string saveContentFromMore()
        {
            clickOnElement(more);
            waitForElementDisplayed(moreList);
            string moreListContent = getTextFromElement(moreList);
            clickOnElement(more);
            waitForElementDisplayed(more);
            return moreListContent;
        }

        public void cityVerify(string cityParis)
        {
            waitForElementDisplayed(more);
            isEqualElements(cityParis, getTextFromElement(geolocation));
        }
    }
}
