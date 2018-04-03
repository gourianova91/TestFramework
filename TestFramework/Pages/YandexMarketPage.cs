using NUnit.Framework;
using OpenQA.Selenium;

namespace TestFramework
{
    class YandexMarketPage : BasePage
    {
        public static By deliveryPrice = By.Id("delivery-included-filter");
        public static By regionModal = By.CssSelector("div.n-region-notification__actions.layout.layout_display_flex > div:nth-child(1) > span");
        public static By rateStore = By.CssSelector("input#qrfrom_4");
        public static By countProducts = By.CssSelector("button[role=listbox]");
        public static By selectListCountProducts = By.XPath("//div[contains(@class, 'select__item')]/span[contains(text(), 'Показывать по 12')]");
        public static By search = By.Id("header-search");
        public static By scrollTo = By.CssSelector("div.copyright");

        public static string value = "12";
        public static string text = "Xiaomi Mi Max";

        public void regionSelect()
        {
            clickOnElement(regionModal);
        }

        public void checkDeliveryPrice()
        {
            waitForElementDisplayed(deliveryPrice);
            checkCheckbox(deliveryPrice);
            uncheckCheckbox(deliveryPrice);
            waitForAjax();
            waitForDocumentReady();
        }
        
        public void viewProductsByHighRate()
        {
            moveToElement(rateStore);
            selectRadioButton(rateStore);
            waitForAjax();
            waitForDocumentReady();
        }

        public void scrollPage()
        {
            scrollPageDown(scrollTo);
            waitForAjax();
            waitForDocumentReady();
        }

        public void selectCountProductsOnPage()
        {
            selectFromList(countProducts, selectListCountProducts, value);
        }

        public void getSearchTextFromMarket()
        {
            Assert.AreEqual(text, getAttributeText(search, text));
        }

        public void acceptAlertIfPresent()
        {
            checkAlert();
        }
    }
}
