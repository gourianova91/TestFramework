using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TestFramework.Core
{
    public class BasePage
    {
        //protected IWebDriver driver;
        private Waiter wait;

        protected BasePage()
        {
            //driver = Driver.Instance.getWebDriver();
            wait = new Waiter();
        }

        public void navigateTo(string url)
        {
            Driver.Instance.getWebDriver().Navigate().GoToUrl(url);
            WaitForAjax();
            WaitForDocumentReady();
        }

        protected void MoveToElement(By selector)
        {
            var elem = Driver.Instance.getWebDriver().FindElement(selector);
            ((IJavaScriptExecutor)Driver.Instance.getWebDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
        }

        private void IsClickable(By selector)
        {
            WaitForElementDisplayed(selector);
            WaitForElementEnabled(selector);
            MoveToElement(selector);
        }

        protected void ClickOnElement(By selector)
        {
            IsClickable(selector);
            Driver.Instance.getWebDriver().FindElement(selector).Click();
        }

        protected void EnterText(By selector, string text)
        {
            WaitForElementDisplayed(selector);
            Driver.Instance.getWebDriver().FindElement(selector).Clear();
            Driver.Instance.getWebDriver().FindElement(selector).SendKeys(text);
        }

        protected void WaitForAutocompleteJquery(By selectorAutocompleteList, By selectorInput)
        {
            wait.IsDisplayed(selectorAutocompleteList);
            Driver.Instance.getWebDriver().FindElement(selectorInput).SendKeys(Keys.ArrowDown + Keys.Enter);
        }

        protected void WaitForElementDisplayed(By selector)
        {
            wait.IsDisplayed(selector);
        }

        protected bool IsElementDisplayed(By selector)
        {
            return wait.IsDisplayed(selector);
        }

        //public bool isNotElementDisplayed(By selector)
        //{
        //    return wait.isNotDisplayed(selector);
        //}

        private void WaitForElementEnabled(By selector)
        {
            wait.IsEnabled(selector);
        }

        public void WaitForElementClicable(By selector)
        {
            wait.IsClicable(selector);
        }

        protected string GetTextFromElement(By selector)
        {
            WaitForElementDisplayed(selector);
            return Driver.Instance.getWebDriver().FindElement(selector).Text;
        }

        protected void IsEqualElements(string firstTextElement, string secondTextElement)
        {
            Assert.AreEqual(firstTextElement, secondTextElement);
        }

        protected void UncheckCheckbox(By selector)
        {
            if (Driver.Instance.getWebDriver().FindElement(selector).Selected)
            {
                IWebElement element = Driver.Instance.getWebDriver().FindElement(selector);
                Actions action = new Actions(Driver.Instance.getWebDriver());
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        protected void CheckCheckbox(By selector)
        {
            if (Driver.Instance.getWebDriver().FindElement(selector).Selected == false)
            {
                IWebElement element = Driver.Instance.getWebDriver().FindElement(selector);
                Actions action = new Actions(Driver.Instance.getWebDriver());
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        protected void WaitForAjax()
        {
            wait.WaitForAjaxToComplete();
        }

        protected void WaitForDocumentReady()
        {
            wait.WaitForDocument();
        }

        protected void SelectRadioButton(By selector)
        {
            if (!Driver.Instance.getWebDriver().FindElement(selector).Selected)
            {
                IWebElement element = Driver.Instance.getWebDriver().FindElement(selector);
                Actions action = new Actions(Driver.Instance.getWebDriver());
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        protected void ScrollPageDown(By selector)
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,750)", "");
            MoveToElement(selector);
            IWebElement element = Driver.Instance.getWebDriver().FindElement(selector);
            Actions action = new Actions(Driver.Instance.getWebDriver());
            action.MoveToElement(element).Perform();
        }

        protected string GetAttributeText(By selector, string text)
        {
            EnterText(selector, text);
            string value = Driver.Instance.getWebDriver().FindElement(selector).GetAttribute("value");
            Driver.Instance.getWebDriver().FindElement(selector).Clear();
            return value;
        }

        protected void SelectFromList(By selectorListButton, By selectorList, string text)
        {
            wait.WaitForAjaxToComplete();
            Driver.Instance.getWebDriver().FindElement(selectorListButton).Click();
            WaitForElementDisplayed(selectorList);
            Driver.Instance.getWebDriver().FindElement(selectorList).Click();
            //SelectElement select = new SelectElement(list);
            //select.SelectByValue(text);
        }

        private bool IsAlertPresent()
        {
            wait.WaitForAlert();
            IAlert alert = CustomExpectedConditions.AlertIsPresent().Invoke(Driver.Instance.getWebDriver());
            return (alert != null);
        }

        protected void CheckAlert()
        {
            if (IsAlertPresent())
            {
                wait.WaitForAlert();
                Driver.Instance.getWebDriver().SwitchTo().Alert().Accept();
            }
        }

        protected string GetUrl()
        {
            return Driver.Instance.getWebDriver().Url;
        }

        protected void GoBack()
        {
            Driver.Instance.getWebDriver().Navigate().Back();
        }
    }
}
