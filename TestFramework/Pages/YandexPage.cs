using NUnit.Framework;
using OpenQA.Selenium;
using TestFramework.Core;

namespace TestFramework.Pages
{
    class YandexPage : BasePage
    {
        private static readonly By Geolocation = By.CssSelector("div.col.headline__item.headline__leftcorner > a");
        private static readonly By City = By.CssSelector("#city__front-input");
        private static readonly By CityList = By.CssSelector("ul.popup__items.input__popup-items");
        private static readonly By More = By.CssSelector("a.home-link.home-link_blue_yes.home-tabs__link.home-tabs__more-switcher");
        private static readonly By MoreList = By.CssSelector("div.home-tabs__more");
        private static readonly By Checkbox = By.CssSelector("input.checkbox__control");

        private static readonly By Video = By.CssSelector("a[data-id='video']");
        private static readonly By Images = By.CssSelector("a[data-id='images']");
        private static readonly By News = By.CssSelector("a[data-id='news']");
        private static readonly By Maps = By.CssSelector("a[data-id='maps']");
        private static readonly By Market = By.CssSelector("a[data-id='market']");
        private static readonly By Translate = By.CssSelector("a[data-id='translate']");
        private static readonly By Music = By.CssSelector("a[data-id='music']");

        private const string UrlVideo = "https://yandex.by/video/";
        private const string UrlImages = "https://yandex.by/images/";
        private const string UrlNews = "https://news.yandex.by/";
        private const string UrlMaps = "https://yandex.by/maps";
        private const string UrlMarket = "https://market.yandex.by/";
        private const string UrlTranslate = "https://translate.yandex.by/";
        private const string UrlMusic = "https://music.yandex.by/";

        private static readonly By Lang = By.CssSelector("span.link__inner > span");
        private static readonly By MoreLang = By.CssSelector("a[aria-label='ещё']");
        private static readonly By LangSelectbtn = By.CssSelector(" div.select.option__select.select_size_m.select_theme_normal.i-bem.select_js_inited > button");
        private static readonly By EngLang = By.XPath("//div[contains(@class, 'select__item')]/span[contains(text(), 'English')]");
        private static readonly By SaveLangbtn = By.CssSelector("button.button.form__save.button_theme_action.button_size_m.i-bem.button_js_inited");

        private const string Eng = "Eng";

        public void ChangeGeolocation(string cityName)
        {
            ClickOnElement(Geolocation);
            UncheckCheckbox(Checkbox);
            EnterText(City, cityName);
            WaitForAutocompleteJquery(CityList, City);
        }

        public string SaveContentFromMore()
        {
            ClickOnElement(More);
            WaitForElementDisplayed(MoreList);
            string moreListContent = GetTextFromElement(MoreList);
            ClickOnElement(More);
            WaitForElementDisplayed(More);
            return moreListContent;
        }

        public void CityVerify(string cityParis)
        {
            WaitForElementDisplayed(More);
            IsEqualElements(cityParis, GetTextFromElement(Geolocation));
        }

        public void GoToVideoAndVerify()
        {
            ClickOnElement(Video);
            Assert.IsTrue(GetUrl().Contains(UrlVideo));
            GoBack();
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void GoToImagesAndVerify()
        {
            ClickOnElement(Images);
            Assert.IsTrue(GetUrl().Contains(UrlImages));
            GoBack();
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void GoToNewsAndVerify()
        {
            ClickOnElement(News);
            Assert.IsTrue(GetUrl().Contains(UrlNews));
            GoBack();
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void goToMapsAndVerify()
        {
            ClickOnElement(Maps);
            Assert.IsTrue(GetUrl().Contains(UrlMaps));
            GoBack();
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void GoToMarketAndVerify()
        {
            ClickOnElement(Market);
            Assert.IsTrue(GetUrl().Contains(UrlMarket));
            GoBack();
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void GoToTranslateAndVerify()
        {
            ClickOnElement(Translate);
            Assert.IsTrue(GetUrl().Contains(UrlTranslate));
            GoBack();
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void GoToMusicAndVerify()
        {
            ClickOnElement(Music);
            Assert.IsTrue(GetUrl().Contains(UrlMusic));
            GoBack();
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void ChangeToEng()
        {
            ClickOnElement(Lang);
            ClickOnElement(MoreLang);
            WaitForAjax();
            WaitForDocumentReady();
            ClickOnElement(LangSelectbtn);
            ClickOnElement(EngLang);
            ClickOnElement(SaveLangbtn);
            WaitForAjax();
            WaitForDocumentReady();
        }

        public void VerifyChangeLang()
        {
            Assert.AreEqual(Eng, GetTextFromElement(Lang));
        }
    }
}
