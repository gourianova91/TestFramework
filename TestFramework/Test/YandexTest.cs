using NUnit.Framework;
using TestFramework.Core;
using TestFramework.Pages;

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

        public YandexTest(Driver.BrowserType browser) : base(browser)
        {
            _yandex = new YandexPage();
            _mail = new YandexMailPage();
        }

        [Test, Order(1)]
        public void ValidLogin()
        {
            _mail.navigateTo(Url);
            _mail.LoginMail(Username, Userpassword);
            _mail.CheckUserName(Username);
            //Postcondition
            _mail.LogoutMail();
        }
                
        [Test, Order(2)]
        public void Logout()
        {
            _mail.navigateTo(Url);
            _mail.LoginMail(Username, Userpassword);
            _mail.LogoutMail();
            _mail.Verifylogout();
        }

        [Test, Order(3)]
        public void NoValidLoginPassword()
        {
            _mail.navigateTo(Url);
            _mail.LoginMail(Username, NotValidUserPassword);
            _mail.VerifyErrorMsgToPasswd();
        }

        [Test, Order(4)]
        public void NoValidLoginName()
        {
            _mail.navigateTo(Url);
            _mail.LoginMail(NotValidUserLogin, Userpassword);
            _mail.VerifyErrorMsgToLogin();
        }

        [Test, Order(5)]
        public void Navigation()
        {
            _yandex.navigateTo(Url);
            _yandex.GoToVideoAndVerify();
            _yandex.GoToImagesAndVerify();
            _yandex.GoToNewsAndVerify();
            _yandex.goToMapsAndVerify();
            _yandex.GoToMarketAndVerify();
            _yandex.GoToTranslateAndVerify();
            _yandex.GoToMusicAndVerify();
        }

        [Test, Order(6)]
        public void ChangeLanguageToEnglish()
        {
            _yandex.navigateTo(Url);
            _yandex.ChangeToEng();
            _yandex.VerifyChangeLang();
        }
    }
}
