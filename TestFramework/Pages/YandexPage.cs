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

        public void compareContent(string cityLondon, string cityParis)
        {
            Waiter wait = new Waiter();
            driver.FindElement(geolocation).Click();

            driver.FindElement(checkbox).Click();
            driver.FindElement(city).Clear();
            driver.FindElement(city).SendKeys(cityLondon);
            wait.isDisplayed(cityList);
            driver.FindElement(city).SendKeys(Keys.ArrowDown + Keys.Enter);

            wait.isDisplayed(more);
            driver.FindElement(more).Click();
            wait.isDisplayed(moreList);
            string moreListLondon = driver.FindElement(moreList).Text;

            driver.FindElement(more).Click();
            wait.isDisplayed(moreList);
            driver.FindElement(geolocation).Click();

            driver.FindElement(city).Clear();
            driver.FindElement(city).SendKeys(cityParis);
            wait.isDisplayed(cityList);
            driver.FindElement(city).SendKeys(Keys.ArrowDown + Keys.Enter);

            wait.isDisplayed(more);

            Assert.AreEqual(cityParis, driver.FindElement(geolocation).Text);

            driver.FindElement(more).Click();
            string moreListParis = driver.FindElement(moreList).Text;

            Assert.True(moreListLondon.Equals(moreListParis));
        } 
    }
}
