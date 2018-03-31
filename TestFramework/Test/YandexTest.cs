using NUnit.Framework;

namespace TestFramework
{
    [Parallelizable]
    [TestFixture(Driver.BrowserType.Chrome)]
    [TestFixture(Driver.BrowserType.Firefox)]
    //[TestFixture(Driver.BrowserType.IE)]
    public class YandexTest : BaseTest
    {
        protected static string url = "https://yandex.ru/";
        protected static string cityLondon = "Лондон";
        protected static string cityParis = "Париж";
        YandexPage yandex;

        public YandexTest(Driver.BrowserType browser) : base(browser)
        {
            yandex = new YandexPage();
        }

        [Test]
        public void yandexTest()
        {
            yandex.navigateTo(url);
            yandex.changeGeolocation(cityLondon);
            string MoreLondon = yandex.saveContentFromMore();
            yandex.changeGeolocation(cityParis);
            yandex.cityVerify(cityParis);
            string MoreParis = yandex.saveContentFromMore();
            Assert.AreEqual(MoreLondon, MoreParis);
        }
    }
}
