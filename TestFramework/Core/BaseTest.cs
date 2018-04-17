using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TestFramework.Report;

namespace TestFramework.Core
{
    public class BaseTest
    {
        private readonly Driver.BrowserType _browser;

        public BaseTest(Driver.BrowserType browser)
        {
            this._browser = browser;
            Driver.Instance.getWebDriver(browser);
            ExtentTestManager.CreateParentTest(GetType().Name + " (" + _browser + ")");
        }

        [SetUp]
        public void SetUp()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
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
                default:
                    logstatus = Status.Pass;
                    break;
            }
            ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            System.Threading.Thread.Sleep(1000);
            Driver.Instance.StopBrowser();
            ExtentManager.Instance.Flush();
        }
    }
}
