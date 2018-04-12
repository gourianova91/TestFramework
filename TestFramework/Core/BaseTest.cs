using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using static System.String;

namespace TestFramework.Core
{
    public class BaseTest
    {
        private readonly Driver.BrowserType _browser;
        private ExtentReports _extent;
        private ExtentTest _test;

        public BaseTest(Driver.BrowserType browser)
        {
            this._browser = browser;
            //Driver.Instance.getWebDriver(browser);
        }

        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
            var dir = TestContext.CurrentContext.TestDirectory + "\\";
            var fileName = this.GetType().ToString() + ".html";
            var htmlReporter = new ExtentHtmlReporter(dir + fileName);

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void SetUp()
        {
            Driver.Instance.getWebDriver(_browser);

            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            System.Threading.Thread.Sleep(1000);
            Driver.Instance.StopBrowser();

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? ""
                : $"{TestContext.CurrentContext.Result.StackTrace}";
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                case TestStatus.Passed:
                    logstatus = Status.Pass;
                    break;
                case TestStatus.Warning:
                    logstatus = Status.Warning;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            _extent.Flush();
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            _extent.Flush();
        }
    }
}
