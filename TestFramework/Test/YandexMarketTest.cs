using NUnit.Framework;

namespace TestFramework
{
    [Parallelizable]
    [TestFixture(Driver.BrowserType.Chrome)]
    [TestFixture(Driver.BrowserType.Firefox)]
    //[TestFixture(Driver.BrowserType.IE)]
    class YandexMarketTest : BaseTest
    {
        protected static string url = "https://market.yandex.by/catalog/54726/list?local-offers-first=0&deliveryincluded=0&onstock=1";
        YandexMarketPage market = new YandexMarketPage();

        public YandexMarketTest(Driver.BrowserType browser)
        {
            this.browser = browser;
        }

        [Test]
        public void yandexMarketTest()
        {
            market.navigateTo(url);
            market.regionSelect();
            market.getSearchTextFromMarket();
            market.checkDeliveryPrice();
            market.viewProductsByList();
            market.scrollPage();
            market.selectCountProductsOnPage();
        }
    }
}
