using NUnit.Framework;
using TestFramework.Core;
using TestFramework.Pages;

namespace TestFramework.Test 
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Driver.BrowserType.Chrome)]
    [TestFixture(Driver.BrowserType.Firefox)]
    //[TestFixture(Driver.BrowserType.IE)]
    class YandexMarketMusicTest : BaseTest
    {
        private static string url = "https://yandex.by/";
        private const int FirstMobile = 0;
        private const int SecondMobile = 1;
        private static string title = "Товаров нет";
        private static string sort = "desc";
        private const int toWidth = 50;
        private static string filter = "filter=15464317%3A~50"; //ширина - до 50см
        private const string Username = "AutotestUser";
        private const string Userpassword = "AutotestUser123";
        private const string Music = "Metal";
        private const string MusicBand = "metallica";
        private const string muBand = "Metallica";
        private const string BMusic = "beyo";
        private const string BMusicBand = "beyoncé";
        private const string bmuBand = "Beyoncé";
        private const int FirstSong = 0;

        private readonly YandexPage _yandex;
        private readonly YandexMarketPage _market;
        private readonly YandexMusicPage _music;
        private readonly YandexMailPage _mail;

        public YandexMarketMusicTest(Driver.BrowserType browser) : base(browser)
        {
            _yandex = new YandexPage();
            _mail = new YandexMailPage();
            _market = new YandexMarketPage();
            _music = new YandexMusicPage();
        }

        [Test, Order(1)]
        public void AdditionToComparison()
        {
            _yandex.navigateTo(url);
            _yandex.GoToYandexMarket();
            _market.SearchForProduct();
            string firstProduct = _market.AddProductToComparaison(FirstMobile);
            _market.CloseComparaisonPopup();
            string secondProduct = _market.AddProductToComparaison(SecondMobile);
            _market.CompareProducts();
            string firstProductCompare = _market.CompareModelProducts(SecondMobile);
            string secondProductCompare = _market.CompareModelProducts(FirstMobile);
            Assert.AreEqual(firstProduct, firstProductCompare);
            Assert.AreEqual(secondProduct, secondProductCompare);
            //PostCondition
            _market.DeleteComparaisonList();
        }

        [Test, Order(2)]
        public void AdditionToComparisonAndDelete()
        {
            _yandex.navigateTo(url);
            _yandex.GoToYandexMarket();
            _market.SearchForProduct();
            string firstProduct = _market.AddProductToComparaison(FirstMobile);
            _market.CloseComparaisonPopup();
            string secondProduct = _market.AddProductToComparaison(SecondMobile);
            _market.CompareProducts();
            string firstProductCompare = _market.CompareModelProducts(SecondMobile);
            string secondProductCompare = _market.CompareModelProducts(FirstMobile);
            Assert.AreEqual(firstProduct, firstProductCompare);
            Assert.AreEqual(secondProduct, secondProductCompare);
            _market.DeleteComparaisonList();
            string noProducts = _market.VerifyDeleteComparaisonList(title);
            Assert.IsTrue(noProducts.Contains(title));
        }

        [Test, Order(3)]
        public void SortByPrice()
        {
            _yandex.navigateTo(url);
            _yandex.GoToYandexMarket();
            _market.ChooseCamerasFromMenu();
            _market.SortByPriceFromHighToLow();
            string sortDesc = _market.VerifySortByPriceFromHighToLow();
            Assert.IsTrue(sortDesc.Contains(sort));
        }

        [Test, Order(4)]
        public void FilterByWidth()
        {
            _yandex.navigateTo(url);
            _yandex.GoToYandexMarket();
            _market.ChooseFridgesFromMenu();
            _market.FilterByWidth(toWidth);
            string filterUrl = _market.VerifyFilterByWidth();
            Assert.IsTrue(filterUrl.Contains(filter));
        }

        [Test, Order(5)]
        public void YandexMusicMetallica()
        {
            //Precondition--------------------------
            _yandex.navigateTo(url);
            _mail.LoginMail(Username, Userpassword);
            //--------------------------------------
            _yandex.navigateTo(url);
            _yandex.GoToYandexMusic();
            _music.SearchFor(Music);
            _music.Choose(MusicBand);
            string mband = _music.VerifyMusicBand();
            Assert.AreEqual(muBand, mband);
            Assert.IsTrue(_music.VerifyArtist(muBand));
        }

        [Test, Order(6)]
        public void YandexMusicPlay()
        {
            //Precondition--------------------------
            _yandex.navigateTo(url);
            _mail.LoginMail(Username, Userpassword);
            //--------------------------------------
            _yandex.navigateTo(url);
            _yandex.GoToYandexMusic();
            _music.SearchFor(BMusic);
            _music.Choose(BMusicBand);
            string bmband = _music.VerifyMusicBand();
            Assert.AreEqual(bmuBand, bmband);
            _music.StartPausePlaying(FirstSong);
            System.Threading.Thread.Sleep(3000);
            _music.StartPausePlaying(FirstSong);
        }
    }
}
