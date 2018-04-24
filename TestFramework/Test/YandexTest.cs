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
    class YandexTest : BaseTest
    {
        private const string Url = "https://yandex.by/";
        private const string Username = "AutotestUser";
        private const string Userpassword = "AutotestUser123";
        private const string NotValidUserPassword = "NoAutotestUser123";
        private const string NotValidUserLogin = "NoAutotestUser";

        private readonly YandexPage _yandex;
        private readonly YandexMailPage _mail;
        Status logstatus;

        public YandexTest(Driver.BrowserType browser) : base(browser)
        {
            _yandex = new YandexPage();
            _mail = new YandexMailPage();
        }

        [Test, Order(1)]
        public void ValidLogin()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _mail.navigateTo(Url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Login yandex mail");
            _mail.LoginMail(Username, Userpassword);
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Check username login");
            _mail.CheckUserName(Username);
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Post-condition: Logout yandex mail");
            _mail.LogoutMail();
        }
                
        [Test, Order(2)]
        public void Logout()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _mail.navigateTo(Url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Login yandex mail");
            _mail.LoginMail(Username, Userpassword);
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Logout yandex mail");
            _mail.LogoutMail();
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Verify logout yandex mail");
            _mail.Verifylogout();
        }

        [Test, Order(3)]
        public void NoValidLoginPassword()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _mail.navigateTo(Url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Login yandex mail with no valid user password");
            _mail.LoginMail(Username, NotValidUserPassword);
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Verify error message for no valid user password");
            _mail.VerifyErrorMsgToPasswd();
        }

        [Test, Order(4)]
        public void NoValidLoginName()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _mail.navigateTo(Url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Login yandex mail with no valid user login");
            _mail.LoginMail(NotValidUserLogin, Userpassword);
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Verify error message for no valid user login");
            _mail.VerifyErrorMsgToLogin();
        }

        [Test, Order(5)]
        public void Navigation()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _yandex.navigateTo(Url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Navigate to the Yandex Video");
            _yandex.GoToVideoAndVerify();
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Navigate to the Yandex Images");
            _yandex.GoToImagesAndVerify();
            ExtentTestManager.GetTest().Log(logstatus, "Step 4: Navigate to the Yandex News");
            _yandex.GoToNewsAndVerify();
            ExtentTestManager.GetTest().Log(logstatus, "Step 5: Navigate to the Yandex maps");
            _yandex.goToMapsAndVerify();
            ExtentTestManager.GetTest().Log(logstatus, "Step 6: Navigate to the Yandex Market");
            _yandex.GoToMarketAndVerify();
            ExtentTestManager.GetTest().Log(logstatus, "Step 7: Navigate to the Yandex Translate");
            _yandex.GoToTranslateAndVerify();
            ExtentTestManager.GetTest().Log(logstatus, "Step 8: Navigate to the Yandex Music");
            _yandex.GoToMusicAndVerify();
        }

        [Test, Order(6)]
        public void ChangeLanguageToEnglish()
        {
            ExtentTestManager.GetTest().Log(logstatus, "Step 1: Go to the Yandex Page");
            _yandex.navigateTo(Url);
            ExtentTestManager.GetTest().Log(logstatus, "Step 2: Change language to Eng");
            _yandex.ChangeToEng();
            ExtentTestManager.GetTest().Log(logstatus, "Step 3: Verify change language to Eng");
            _yandex.VerifyChangeLang();
        }
    }
}
