using NUnit.Framework;

namespace TestFramework
{
    [Parallelizable]
    [TestFixture(Driver.BrowserType.Chrome)]
    [TestFixture(Driver.BrowserType.Firefox)]
    //[TestFixture(Driver.BrowserType.IE)]
    public class BBCTest : BaseTest
    {
        protected static string url = "http://www.bbc.com/";
        protected static string text = "Sherlock";
        BBCPage bbc = new BBCPage();

        public BBCTest(Driver.BrowserType browser)
        {
            this.browser = browser;
        }

        [Test]
        public void bbcTest()
        {
            bbc.navigateTo(url);
            bbc.searchText(text);
        }
    }
}
