using NUnit.Framework;
using OpenQA.Selenium;
using System;
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
        private static readonly By AddToComparaison = By.CssSelector("div.n-product-toolbar.i-bem.n-product-toolbar_label_no.n-product-toolbar_js_inited > div:first-of-type");
        private static readonly By ModelProduct = By.CssSelector("div.n-snippet-cell2__title > a:first-of-type");
        private static readonly By Compare = By.CssSelector("a.button.button_size_m.button_theme_normal.i-bem.button_js_inited");
        private static readonly By CompareModelProduct = By.CssSelector("a.n-compare-head__name.link");
        private static readonly By ClosingComparaisonPopup = By.CssSelector("div.popup-informer__close.image.image_name_close");
        private static readonly By DeletingComparaisonList = By.CssSelector("span.n-compare-toolbar__action-clear.link > span.link__inner");
        private static readonly By ByPrice = By.XPath("//a[contains(text(), 'по цене')]");
        private static readonly By Electronics = By.XPath("//a[contains(text(), 'Электроника')][@href='/catalog/54440?hid=198119&track=menu']");
        private static readonly By ActionCameras = By.XPath("//div[contains(@class, 'catalog-menu__list')]/a[contains(text(), 'Экшн-камеры')]");
        private static readonly By Appliances = By.XPath("//a[contains(text(), 'Бытовая техника')][@href='/catalog/54419?hid=198118&track=menu']");
        private static readonly By Fridge = By.XPath("//a[contains(text(), 'Крупная техника для кухни')]/ancestor::div[contains(@class, 'catalog-menu__item')]//a[contains(text(), 'Холодильники')]");
        private static readonly By ToWidth = By.Id("15464317to");

        private static readonly string SortingByPrice = "//div[contains(@class, 'n-filter-sorter i-bem n-filter-sorter_js_inited n-filter-sorter_sort_desc n-filter-sorter_state_select')]";
        private static readonly string NoProducts = "//div[contains(text(), '{0}')]";

        private const string Value = "12";
        private const string Text = "Xiaomi Mi Max";

        private const string Mobile = "Note 8";

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

        public void SearchForProduct()
        {
            EnterTextAndClickEnter(Search, Mobile);
        }

        public string AddProductToComparaison(int product)
        {
            ClickOnHoverElement(AddToComparaison, product);
            return GetTextFromElement(ModelProduct, product);
        }

        public void CompareProducts()
        {
            ClickOnElement(Compare);
            WaitForAjax();
            WaitForDocumentReady();
        }

        public string CompareModelProducts(int product)
        {
            return GetTextFromElement(CompareModelProduct, product);
        }

        public void CloseComparaisonPopup()
        {
            ClickOnElement(ClosingComparaisonPopup);
        }

        public void DeleteComparaisonList()
        {
            ClickOnElement(DeletingComparaisonList);
            WaitForAjax();
            WaitForDocumentReady();
        }

        public string VerifyDeleteComparaisonList(string text)
        {
            string element = String.Format(NoProducts, text);
            Assert.IsTrue(IsElementDisplayed(By.XPath(element)));
            return element;
        }

        public void ChooseCamerasFromMenu()
        {
            ClickOnElement(Electronics);
            WaitForAjax();
            WaitForDocumentReady();
            ClickOnElement(ActionCameras);
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void SortByPriceFromHighToLow()
        {
            ClickOnElement(ByPrice);
            ClickOnElement(ByPrice);
        }

        public string VerifySortByPriceFromHighToLow()
        {
            Assert.IsTrue(IsElementDisplayed(By.XPath(SortingByPrice)));
            return SortingByPrice;
        }

        public void ChooseFridgesFromMenu()
        {
            ClickOnElement(Appliances);
            WaitForAjax();
            WaitForDocumentReady();
            ClickOnElement(Fridge);
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void FilterByWidth(int width)
        {
            EnterText(ToWidth, width.ToString());
            WaitForAjax();
            WaitForDocumentReady();
        }

        public string VerifyFilterByWidth()
        {
            return GetUrl();
        }
    }
}
