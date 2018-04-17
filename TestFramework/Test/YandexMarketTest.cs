using NUnit.Framework;
using TestFramework.Core;
using TestFramework.Pages;

namespace TestFramework.Test
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Driver.BrowserType.Chrome)]
    [TestFixture(Driver.BrowserType.Firefox)]
    //[TestFixture(Driver.BrowserType.IE)]
    class YandexMarketTest : BaseTest
    {
        private static string url = "https://market.yandex.by/catalog/54726/list?local-offers-first=0&deliveryincluded=0&onstock=1";
        private readonly YandexMarketPage _market;

        public YandexMarketTest(Driver.BrowserType browser) : base(browser)
        {
            _market = new YandexMarketPage();
        }

        [Test]
        public void YandexMarket()
        {
            _market.navigateTo(url);
            _market.RegionSelect();
            _market.SelectCountProductsOnPage();
            _market.AcceptAlertIfPresent();
            _market.GetSearchTextFromMarket();
            _market.CheckDeliveryPrice();
            _market.ViewProductsByHighRate();
            _market.ScrollPage();
        }
    }
}
