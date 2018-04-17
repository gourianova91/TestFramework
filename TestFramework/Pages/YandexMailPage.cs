using NUnit.Framework;
using OpenQA.Selenium;
using TestFramework.Core;

namespace TestFramework.Pages
{
    class YandexMailPage : BasePage
    {
        private static readonly By Mail = By.CssSelector("div.desk-notif-card__login-title > a.home-link.home-link_black_yes");
        private static readonly By Login = By.CssSelector("input[name='login']");
        private static readonly By Passwd = By.CssSelector("input[type='password']");
        private static readonly By Loginbtn = By.CssSelector("button[type='submit']");
        private static readonly By Username = By.CssSelector("div.mail-User-Name");
        private static readonly By Logoutbtn = By.CssSelector("a[data-metric='Выход']");
        private static readonly By UsrloginMail = By.CssSelector("div.mail-User-Name");
        private static readonly By UsrloginYandex = By.CssSelector("span.username.desk-notif-card__user-name");
        private static readonly By ErrorMsg = By.CssSelector("div.passport-Domik-Form-Error.passport-Domik-Form-Error_active");
        private static readonly By ForeignComputer = By.CssSelector("input.passport-Checkbox-Controller");

        private const string ErrMsgPasswd = "Неверный пароль";
        private const string ErrMsgLogin = "Такого аккаунта нет";

        public void LoginMail(string username, string password)
        {
            ClickOnElement(Mail);
            EnterText(Login, username);
            EnterText(Passwd, password);
            CheckCheckbox(ForeignComputer);
            ClickOnElement(Loginbtn);
        }

        public void CheckUserName(string login)
        {
            Assert.AreEqual(login, GetTextFromElement(Username));
        }

        public void LogoutMail()
        {
            WaitForAjax();
            WaitForDocumentReady();
            ClickOnElement(UsrloginMail);
            ClickOnElement(Logoutbtn);
        }

        public void Verifylogout()
        {
            Assert.IsFalse(IsElementDisplayed(UsrloginYandex));
        }

        public void VerifyErrorMsgToPasswd()
        {
            Assert.AreEqual(ErrMsgPasswd, GetTextFromElement(ErrorMsg));
        }

        public void VerifyErrorMsgToLogin()
        {
            Assert.AreEqual(ErrMsgLogin, GetTextFromElement(ErrorMsg));
        }
    }
}
