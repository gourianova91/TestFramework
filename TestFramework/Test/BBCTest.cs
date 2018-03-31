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
        BBCPage bbc;

        public BBCTest(Driver.BrowserType browser) : base(browser)
        {
            bbc = new BBCPage();
        }

        [Test]
        public void bbcTest()
        {
            bbc.navigateTo(url);
            bbc.searchText(text);
        }
    }
}
