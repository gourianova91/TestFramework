using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TestFramework.Core;

namespace TestFramework.Pages
{
    class YandexMusicPage : BasePage
    {
        private static readonly By SearchMusic = By.CssSelector("input.nb-input._nb-simple-input._init.head__suggest-input.ui-autocomplete-input");
        private static readonly By BandName = By.CssSelector("h1.page-artist__title.typo-h1_big");
        private static readonly By PlayFirstSong = By.XPath("//span[contains(text(), 'Популярные треки')]/ancestor::div[contains(@class, 'page-artist__latest-main')]//div[contains(@class, 'd-track typo-track d-track_selectable d-track_inline-meta d-track_with-cover')]//button[contains(@data-b, '1097')]");

        private static readonly string Artist = "//div[contains(@class, 'album__artist')]/a[contains(text(), '{0}')]";
        private static readonly string SearchAutocomplete = "//ul[contains(@class, 'ui-autocomplete ui-front ui-menu ui-widget ui-widget-content ui-corner-all nb-island nb-island_type_fly')]//a[text()='{0}']";

        public void SearchFor(string text)
        {
            EnterText(SearchMusic, text);
        }

        public void Choose(string band)
        {
            ClickOnElement(By.XPath(String.Format(SearchAutocomplete, band)));
            WaitForAjax();
            WaitForDocumentReady();
        }

        public string VerifyMusicBand()
        {
            return GetTextFromElement(BandName);
        }

        public bool VerifyArtist(string name)
        {
            IList<string> names = GetElements(By.XPath(String.Format(Artist, name)));
            return CheckElementsList(name, names);
        }

        public void StartPausePlaying(int number)
        {
            ClickOnHoverElement(PlayFirstSong, number);
        }
    }
}
