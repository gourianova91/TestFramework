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
    class YandexMarketTest : BaseTest
    {
        private static string url = "https://market.yandex.by/catalog/54726/list?local-offers-first=0&deliveryincluded=0&onstock=1";
        private readonly YandexMarketPage _market;
        Status logstatus;

        public YandexMarketTest(Driver.BrowserType browser) : base(browser)
        {
            _market = new YandexMarketPage();
        }

        [Test]
        public void YandexMarket()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Market Page");
            _market.navigateTo(url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Change the count of products on page");
            _market.RegionSelect();
            _market.SelectCountProductsOnPage();
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Set text and get it from the search input");
            _market.AcceptAlertIfPresent();
            _market.GetSearchTextFromMarket();
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Check delivery price in checkbox");
            _market.CheckDeliveryPrice();
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Select the high rate products by radiobutton");
            _market.ViewProductsByHighRate();
            ExtentTestManager.GetTest().Log(logstatus, "Step 6: Scroll page down to copyright");
            _market.ScrollPage();
        }
    }
}
