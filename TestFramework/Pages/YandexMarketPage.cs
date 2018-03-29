using OpenQA.Selenium;
using System.Threading;

namespace TestFramework
{
    class YandexMarketPage : BasePage
    {
        public static By deliveryPrice = By.Id("delivery-included-filter");
        public static By regionModal = By.CssSelector("div.n-region-notification__actions.layout.layout_display_flex > div:nth-child(1) > span");
        public static By rateStore = By.Id("qrfrom_4");
        public static By countProducts = By.CssSelector("button[role=listbox]");
        public static By selectCountProducts = By.CssSelector("option[value='12']");

        public void regionSelect()
        {
            clickOnElement(regionModal);
        }

        public void checkDeliveryPrice()
        {
            checkCheckbox(deliveryPrice);
            uncheckCheckbox(deliveryPrice);
            waitForAjax();
            waitForDocumentReady();
        }
        
        public void viewProductsByList()
        {
            selectRadioButton(rateStore);
            waitForAjax();
            waitForDocumentReady();
        }

        public void scrollPage()
        {
            scrollPageDown();
            waitForAjax();
            waitForDocumentReady();
        }

        public void selectCountProductsOnPage()
        {
            selectFromList(countProducts, selectCountProducts);
            waitForDocumentReady();
            Thread.Sleep(3000);
        }
    }
}
