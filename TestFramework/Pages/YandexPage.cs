using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace TestFramework
{
    class YandexPage : BasePage
    {
        public static By geolocation = By.CssSelector("div.col.headline__item.headline__leftcorner > a");
        public static By city = By.CssSelector("#city__front-input");
        public static By more = By.CssSelector("a.home-link.home-link_blue_yes.home-tabs__link.home-tabs__more-switcher.dropdown-menu.dropdown-menu__switcher.i-bem.dropdown-menu_js_inited.dropdown-menu_action_closed");
        public static By moreList = By.CssSelector("a.home-link.home-link_blue_yes.home-tabs__link.home-tabs__more-switcher.dropdown-menu.dropdown-menu__switcher.i-bem.dropdown-menu_js_inited.dropdown-menu_action_open");

        public void compareContent(string cityLondon, string cityParis)
        {
            driver.FindElement(geolocation).Click();
            driver.FindElement(city).Clear();
            driver.FindElement(city).SendKeys(cityLondon);
            driver.FindElement(city).SendKeys(Keys.Enter);
            driver.FindElement(more).Click();
            Waiter wait = new Waiter();
            Assert.True(wait.isDisplayed(moreList));
            IList<IWebElement> moreListForLondon = driver.FindElements(moreList);
            driver.FindElement(more).Click();
            Assert.False(wait.isDisplayed(moreList));
            driver.FindElement(geolocation).Click();
            driver.FindElement(city).Clear();
            driver.FindElement(city).SendKeys(cityParis);
            driver.FindElement(city).SendKeys(Keys.Enter);
            driver.FindElement(more).Click();
            IList<IWebElement> moreListForParis = driver.FindElements(moreList);
        } 
    }
}
