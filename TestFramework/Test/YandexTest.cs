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

        public YandexTest(Driver.BrowserType browser)
        {
            this.browser = browser;
        }

        [Test]
        public void yandexTest()
        {
            YandexPage yandex = new YandexPage();
            yandex.navigateTo(url);
            yandex.compareContent(cityLondon, cityParis);
        }
    }
}
