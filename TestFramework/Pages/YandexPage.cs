using OpenQA.Selenium;
using NUnit.Framework;

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

        public static By video = By.CssSelector("a[data-id='video']");
        public static By images = By.CssSelector("a[data-id='images']");
        public static By news = By.CssSelector("a[data-id='news']");
        public static By maps = By.CssSelector("a[data-id='maps']");
        public static By market = By.CssSelector("a[data-id='market']");
        public static By translate = By.CssSelector("a[data-id='translate']");
        public static By music = By.CssSelector("a[data-id='music']");

        public static string urlVideo = "https://yandex.by/video/";
        public static string urlImages = "https://yandex.by/images/";
        public static string urlNews = "https://news.yandex.by/";
        public static string urlMaps = "https://yandex.by/maps";
        public static string urlMarket = "https://market.yandex.by/";
        public static string urlTranslate = "https://translate.yandex.by/";
        public static string urlMusic = "https://music.yandex.by/";

        public static By lang = By.CssSelector("span.link__inner > span");
        public static By moreLang = By.CssSelector("a[aria-label='ещё']");
        public static By langSelectbtn = By.CssSelector(" div.select.option__select.select_size_m.select_theme_normal.i-bem.select_js_inited > button");
        public static By engLang = By.XPath("//div[contains(@class, 'select__item')]/span[contains(text(), 'English')]");
        public static By saveLangbtn = By.CssSelector("button.button.form__save.button_theme_action.button_size_m.i-bem.button_js_inited");

        public static string eng = "Eng";

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

        public void goToVideoAndVerify()
        {
            clickOnElement(video);
            Assert.IsTrue(getUrl().Contains(urlVideo));
            goBack();
            waitForAjax();
            waitForDocumentReady();
        }

        public void goToImagesAndVerify()
        {
            clickOnElement(images);
            Assert.IsTrue(getUrl().Contains(urlImages));
            goBack();
            waitForAjax();
            waitForDocumentReady();
        }

        public void goToNewsAndVerify()
        {
            clickOnElement(news);
            Assert.IsTrue(getUrl().Contains(urlNews));
            goBack();
            waitForAjax();
            waitForDocumentReady();
        }

        public void goToMapsAndVerify()
        {
            clickOnElement(maps);
            Assert.IsTrue(getUrl().Contains(urlMaps));
            goBack();
            waitForAjax();
            waitForDocumentReady();
        }

        public void goToMarketAndVerify()
        {
            clickOnElement(market);
            Assert.IsTrue(getUrl().Contains(urlMarket));
            goBack();
            waitForAjax();
            waitForDocumentReady();
        }

        public void goToTranslateAndVerify()
        {
            clickOnElement(translate);
            Assert.IsTrue(getUrl().Contains(urlTranslate));
            goBack();
            waitForAjax();
            waitForDocumentReady();
        }

        public void goToMusicAndVerify()
        {
            clickOnElement(music);
            Assert.IsTrue(getUrl().Contains(urlMusic));
            goBack();
            waitForAjax();
            waitForDocumentReady();
        }

        public void changeToEng()
        {
            clickOnElement(lang);
            clickOnElement(moreLang);
            waitForAjax();
            waitForDocumentReady();
            clickOnElement(langSelectbtn);
            clickOnElement(engLang);
            clickOnElement(saveLangbtn);
            waitForAjax();
            waitForDocumentReady();
        }

        public void verifyChangeLang()
        {
            Assert.AreNotEqual(eng, getTextFromElement(lang));
        }
    }
}
