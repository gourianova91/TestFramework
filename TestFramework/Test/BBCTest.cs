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
    public class BbcTest : BaseTest
    {
        private const string Url = "http://www.bbc.com/";
        private const string Text = "Sherlock";
        readonly BBCPage _bbc;
        Status logstatus;

        public BbcTest(Driver.BrowserType browser) : base(browser)
        {
            _bbc = new BBCPage();
        }

        [Test]
        public void Bbc()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the BBC Page");
            _bbc.navigateTo(Url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Search for the 'Sherlock'");
            _bbc.SearchText(Text);
        }
    }
}
