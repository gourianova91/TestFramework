using NUnit.Framework;

namespace TestFramework
{
    [Parallelizable]
    [TestFixture(Driver.BrowserType.Chrome)]
    [TestFixture(Driver.BrowserType.Firefox)]
    //[TestFixture(Driver.BrowserType.IE)]
    class YandexTest : BaseTest
    {
        protected static string url = "https://yandex.by/";
        protected static string username = "AutotestUser";
        protected static string userpassword = "AutotestUser123";
        protected static string notValidUserPassword = "NoAutotestUser123";
        protected static string notValidUserLogin = "NoAutotestUser";

        YandexPage yandex;
        YandexMailPage mail;

        public YandexTest(Driver.BrowserType browser) : base(browser)
        {
            yandex = new YandexPage();
            mail = new YandexMailPage();
        }

        [Test, Order(1)]
        public void validLogin()
        {
            mail.navigateTo(url);
            mail.loginMail(username, userpassword);
            mail.checkUserName(username);
        }

        [Test, Order(2)]
        public void logout()
        {
            mail.navigateTo(url);
            mail.loginMail(username, userpassword);
            mail.logoutMail();
            mail.verifylogout();
        }

        [Test, Order(3)]
        public void noValidLoginPassword()
        {
            mail.navigateTo(url);
            mail.loginMail(username, notValidUserPassword);
            mail.verifyErrorMsgToPasswd();
        }

        [Test, Order(4)]
        public void noValidLoginName()
        {
            mail.navigateTo(url);
            mail.loginMail(notValidUserLogin, userpassword);
            mail.verifyErrorMsgToLogin();
        }

        [Test, Order(5)]
        public void navigation()
        {
            yandex.navigateTo(url);
            yandex.goToVideoAndVerify();
            yandex.goToImagesAndVerify();
            yandex.goToNewsAndVerify();
            yandex.goToMapsAndVerify();
            yandex.goToMarketAndVerify();
            yandex.goToTranslateAndVerify();
            yandex.goToMusicAndVerify();
        }

        [Test, Order(6)]
        public void changeLanguageToEnglish()
        {

        }
    }
}
