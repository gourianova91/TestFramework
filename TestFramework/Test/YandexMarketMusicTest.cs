using AventStack.ExtentReports;
using NUnit.Framework;
using TestFramework.Core;
using TestFramework.Pages;
using TestFramework.Report;

namespace TestFramework.Test 
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Driver.BrowserType.Chrome)]
    [TestFixture(Driver.BrowserType.Firefox)]
    //[TestFixture(Driver.BrowserType.IE)]
    class YandexMarketMusicTest : BaseTest
    {
        private static string url = "https://yandex.by/";
        private const string Mobile = "Note 8";
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
        Status logstatus;

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
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _yandex.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Go to the Yandex Market");
            _yandex.GoToYandexMarket();
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Search for 'Note 8'");
            _market.SearchForProduct(Mobile);
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Add first of 'Note 8' products to comparaison");
            string firstProduct = _market.AddProductToComparaison(FirstMobile);
            _market.CloseComparaisonPopup();
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Add second of 'Note 8' products to comparaison");
            string secondProduct = _market.AddProductToComparaison(SecondMobile);
            ExtentTestManager.GetTest().Log(logstatus, "Step 6: Go to the comparaison of two products");
            _market.CompareProducts();
            ExtentTestManager.GetTest().Log(logstatus, "Step 7: Verify that added the correct first product");
            string firstProductCompare = _market.CompareModelProducts(SecondMobile);
            Assert.AreEqual(firstProduct, firstProductCompare);
            ExtentTestManager.GetTest().Log(logstatus, "Step 8: Verify that added the correct second product");
            string secondProductCompare = _market.CompareModelProducts(FirstMobile);
            Assert.AreEqual(secondProduct, secondProductCompare);
            ExtentTestManager.GetTest().Log(logstatus, "Step 9: Post-condition: delete products from comparaison");
            _market.DeleteComparaisonList();
        }

        [Test, Order(2)]
        public void AdditionToComparisonAndDelete()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _yandex.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Go to the Yandex Market");
            _yandex.GoToYandexMarket();
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Search for 'Note 8'");
            _market.SearchForProduct(Mobile);
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Add first of 'Note 8' products to comparaison");
            string firstProduct = _market.AddProductToComparaison(FirstMobile);
            _market.CloseComparaisonPopup();
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Add second of 'Note 8' products to comparaison");
            string secondProduct = _market.AddProductToComparaison(SecondMobile);
            ExtentTestManager.GetTest().Log(logstatus, "Step 6: Go to the comparaison of two products");
            _market.CompareProducts();
            ExtentTestManager.GetTest().Log(logstatus, "Step 7: Verify that added the correct first product");
            string firstProductCompare = _market.CompareModelProducts(SecondMobile);
            Assert.AreEqual(firstProduct, firstProductCompare);
            ExtentTestManager.GetTest().Log(logstatus, "Step 8: Verify that added the correct second product");
            string secondProductCompare = _market.CompareModelProducts(FirstMobile);
            Assert.AreEqual(secondProduct, secondProductCompare);
            ExtentTestManager.GetTest().Log(logstatus, "Step 9: Delete products from comparaison");
            _market.DeleteComparaisonList();
            ExtentTestManager.GetTest().Log(logstatus, "Step 10: Verify deleting products from comparaison");
            string noProducts = _market.VerifyDeleteComparaisonList(title);
            Assert.IsTrue(noProducts.Contains(title));
        }

        [Test, Order(3)]
        public void SortByPrice()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _yandex.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Go to the Yandex Market");
            _yandex.GoToYandexMarket();
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Choose Action cameras from menu");
            _market.ChooseCamerasFromMenu();
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Sort by price from high to low");
            _market.SortByPriceFromHighToLow();
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Verify sort by price from high to low");
            string sortDesc = _market.VerifySortByPriceFromHighToLow();
            Assert.IsTrue(sortDesc.Contains(sort));
        }

        [Test, Order(4)]
        public void FilterByWidth()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _yandex.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Go to the Yandex Market");
            _yandex.GoToYandexMarket();
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Choose Fridges from menu");
            _market.ChooseFridgesFromMenu();
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Filter by width '50 cm'");
            _market.FilterByWidth(toWidth);
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Verify filter by width '50 cm'");
            string filterUrl = _market.VerifyFilterByWidth();
            Assert.IsTrue(filterUrl.Contains(filter));
        }

        [Test, Order(5)]
        public void YandexMusicMetallica()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Pre-condition: Go to the Yandex Page");
            _yandex.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Pre-condition: login in yandex mail");
            _mail.LoginMail(Username, Userpassword);
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Go to the Yandex Page");
            _yandex.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Go to the Yandex Music");
            _yandex.GoToYandexMusic();
            _music.Refresh();
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Search for the 'Metal'");
            _music.SearchFor(Music);
            ExtentTestManager.GetTest().Log(logstatus, "Step 6: Choose 'metallica' from the list");
            _music.Choose(MusicBand);
            ExtentTestManager.GetTest().Log(logstatus, "Step 7: Verify 'Metallica' music band");
            string mband = _music.VerifyMusicBand();
            Assert.AreEqual(muBand, mband);
            ExtentTestManager.GetTest().Log(logstatus, "Step 8: Verify artist 'Metallica' in the Popular songs list");
            Assert.IsTrue(_music.VerifyArtist(muBand));
            ExtentTestManager.GetTest().Log(logstatus, "Step 9: Post-condition: logout yandex mail");
            _music.LogoutMail();
        }

        [Test, Order(6)]
        public void YandexMusicPlay()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Pre-condition: Go to the Yandex Page");
            _yandex.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Pre-condition: login in yandex mail");
            _mail.LoginMail(Username, Userpassword);
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Go to the Yandex Page");
            _yandex.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Go to the Yandex Music");
            _yandex.GoToYandexMusic();
            _music.Refresh();
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Search for the 'beyo'");
            _music.SearchFor(BMusic);
            ExtentTestManager.GetTest().Log(logstatus, "Step 6: Choose 'beyoncé' from the list");
            _music.Choose(BMusicBand);
            ExtentTestManager.GetTest().Log(logstatus, "Step 7: Verify 'Beyoncé' music band");
            string bmband = _music.VerifyMusicBand();
            Assert.AreEqual(bmuBand, bmband);
            ExtentTestManager.GetTest().Log(logstatus, "Step 8: Start playing the first song of Beyoncé");
            _music.StartPausePlaying(FirstSong);
            ExtentTestManager.GetTest().Log(logstatus, "Step 9: Verify start playing of the first song of Beyoncé");
            Assert.IsTrue(_music.VerifyPlaying());
            System.Threading.Thread.Sleep(3000);
            ExtentTestManager.GetTest().Log(logstatus, "Step 10: Pause playing the first song of Beyoncé");
            _music.StartPausePlaying(FirstSong);
            ExtentTestManager.GetTest().Log(logstatus, "Step 11: Verify pause playing of the first song of Beyoncé");
            Assert.IsTrue(_music.VerifyPause());
            ExtentTestManager.GetTest().Log(logstatus, "Step 12: Post-condition: logout yandex mail");
            _music.LogoutMail();
        }
    }
}
