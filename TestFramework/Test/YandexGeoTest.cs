using NUnit.Framework;
using TestFramework.Core;
using TestFramework.Pages;

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

        public YandexGeoTest(Driver.BrowserType browser) : base(browser)
        {
            _yandex = new YandexPage();
        }

        [Test]
        public void Yandex()
        {
            _yandex.navigateTo(Url);
            _yandex.ChangeGeolocation(CityLondon);
            string moreLondon = _yandex.SaveContentFromMore();
            _yandex.ChangeGeolocation(CityParis);
            _yandex.CityVerify(CityParis);
            string moreParis = _yandex.SaveContentFromMore();
            Assert.AreEqual(moreLondon, moreParis);
        }
    }
}
