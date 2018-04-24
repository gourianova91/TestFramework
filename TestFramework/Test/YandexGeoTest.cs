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
    public class YandexGeoTest : BaseTest
    {
        private const string Url = "https://yandex.ru/";
        private const string CityLondon = "Лондон";
        private const string CityParis = "Париж";
        private readonly YandexPage _yandex;
        Status logstatus;

        public YandexGeoTest(Driver.BrowserType browser) : base(browser)
        {
            _yandex = new YandexPage();
        }

        [Test]
        public void Yandex()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _yandex.navigateTo(Url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Change your city to London");
            _yandex.ChangeGeolocation(CityLondon);
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Save content from 'More' with London city");
            string moreLondon = _yandex.SaveContentFromMore();
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Change your city to Paris");
            _yandex.ChangeGeolocation(CityParis);
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Verify changing city to Paris");
            _yandex.CityVerify(CityParis);
            ExtentTestManager.GetTest().Log(logstatus, "Step 6: Save content from 'More' with Paris city");
            string moreParis = _yandex.SaveContentFromMore();
            ExtentTestManager.GetTest().Log(logstatus, "Step 7: Verify equality of content from 'More' with Paris and London city");
            Assert.AreEqual(moreLondon, moreParis);
        }
    }
}
