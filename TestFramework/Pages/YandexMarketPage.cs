using NUnit.Framework;
using OpenQA.Selenium;
using TestFramework.Core;

namespace TestFramework.Pages
{
    class YandexMarketPage : BasePage
    {
        private static readonly By DeliveryPrice = By.Id("delivery-included-filter");
        private static readonly By RegionModal = By.CssSelector("div.n-region-notification__actions.layout.layout_display_flex > div:nth-child(1) > span");
        private static readonly By RateStore = By.CssSelector("input#qrfrom_4");
        private static readonly By CountProducts = By.CssSelector("button[role=listbox]");
        private static readonly By SelectListCountProducts = By.XPath("//div[contains(@class, 'select__item')]/span[contains(text(), 'Показывать по 12')]");
        private static readonly By Search = By.Id("header-search");
        private static readonly By ScrollTo = By.CssSelector("div.copyright");

        private const string Value = "12";
        private const string Text = "Xiaomi Mi Max";

        public void RegionSelect()
        {
            ClickOnElement(RegionModal);
        }

        public void CheckDeliveryPrice()
        {
            WaitForElementDisplayed(DeliveryPrice);
            CheckCheckbox(DeliveryPrice);
            UncheckCheckbox(DeliveryPrice);
            WaitForAjax();
            WaitForDocumentReady();
        }
        
        public void ViewProductsByHighRate()
        {
            MoveToElement(RateStore);
            SelectRadioButton(RateStore);
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void ScrollPage()
        {
            ScrollPageDown(ScrollTo);
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void SelectCountProductsOnPage()
        {
            SelectFromList(CountProducts, SelectListCountProducts, Value);
        }

        public void GetSearchTextFromMarket()
        {
            Assert.AreEqual(Text, GetAttributeText(Search, Text));
        }

        public void AcceptAlertIfPresent()
        {
            CheckAlert();
        }
    }
}
