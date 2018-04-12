using NUnit.Framework;
using TestFramework.Core;
using TestFramework.Pages;

namespace TestFramework.Test
{
    //[Parallelizable]
    [TestFixture(Driver.BrowserType.Chrome)]
    public class BbcTest : BaseTest
    {
        private const string Url = "http://www.bbc.com/";
        private const string Text = "Sherlock";
        readonly BBCPage _bbc;

        public BbcTest(Driver.BrowserType browser) : base(browser)
        {
            _bbc = new BBCPage();
        }

        [Test]
        public void Bbc()
        {
            _bbc.navigateTo(Url);
            _bbc.SearchText(Text);
        }
    }
}
