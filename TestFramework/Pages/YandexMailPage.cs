using NUnit.Framework;
using OpenQA.Selenium;

namespace TestFramework
{
    class YandexMailPage : BasePage
    {
        public static By mail = By.CssSelector("div.desk-notif-card__login-title > a.home-link.home-link_black_yes");
        public static By login = By.CssSelector("input[name='login']");
        public static By passwd = By.CssSelector("input[type='password']");
        public static By loginbtn = By.CssSelector("button[type='submit']");
        public static By username = By.CssSelector("div.mail-User-Name");
        public static By logoutbtn = By.CssSelector("a[data-metric='Выход']");
        public static By usrloginMail = By.CssSelector("div.mail-User-Name");
        public static By usrloginYandex = By.CssSelector("span.username.desk-notif-card__user-name");
        public static By errorMsg = By.CssSelector("div.passport-Domik-Form-Error.passport-Domik-Form-Error_active");

        public static string errMsgPasswd = "Неверный пароль";
        public static string errMsgLogin = "Такого аккаунта нет";

        public void loginMail(string username, string password)
        {
            clickOnElement(mail);
            enterText(login, username);
            enterText(passwd, password);
            clickOnElement(loginbtn);
        }

        public void checkUserName(string login)
        {
            Assert.AreEqual(login, getTextFromElement(username));
        }

        public void logoutMail()
        {
            waitForAjax();
            waitForDocumentReady();
            clickOnElement(usrloginMail);
            clickOnElement(logoutbtn);
        }

        public void verifylogout()
        {
            Assert.IsFalse(isElementDisplayed(usrloginYandex));
        }

        public void verifyErrorMsgToPasswd()
        {
            Assert.AreEqual(errMsgPasswd, getTextFromElement(errorMsg));
        }

        public void verifyErrorMsgToLogin()
        {
            Assert.AreEqual(errMsgLogin, getTextFromElement(errorMsg));
        }
    }
}
