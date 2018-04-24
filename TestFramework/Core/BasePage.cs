using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;

namespace TestFramework.Core
{
    public class BasePage
    {
        protected IWebDriver driver;
        private Waiter wait;

        protected BasePage()
        {
            driver = Driver.Instance.getWebDriver();
            wait = new Waiter();
        }

        public void navigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitForAjax();
            WaitForDocumentReady();
        }

        protected void MoveToElement(By selector)
        {
            var elem =  driver.FindElement(selector);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
        }

        protected void MoveToElement(By selector, int number)
        {
            var elem = driver.FindElements(selector)[number];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
        }

        private void IsClickable(By selector)
        {
            WaitForElementDisplayed(selector);
            WaitForElementEnabled(selector);
            MoveToElement(selector);
        }

        private void IsClickable(By selector, int number)
        {
            WaitForElementDisplayed(selector, number);
            WaitForElementEnabled(selector, number);
            MoveToElement(selector, number);
        }

        protected void ClickOnElement(By selector)
        {
            IsClickable(selector);
            driver.FindElement(selector).Click();
        }

        protected void ClickOnElement(By selector, int number)
        {
            IsClickable(selector, number);
            driver.FindElements(selector)[number].Click();
        }

        protected void EnterText(By selector, string text)
        {
            WaitForElementDisplayed(selector);
            driver.FindElement(selector).Clear();
            driver.FindElement(selector).SendKeys(text);
        }

        protected void EnterTextAndClickEnter(By selector, string text)
        {
            WaitForElementDisplayed(selector);
            driver.FindElement(selector).Clear();
            driver.FindElement(selector).SendKeys(text + Keys.Enter);
        }

        protected void WaitForAutocompleteJquery(By selectorAutocompleteList, By selectorInput)
        {
            wait.IsDisplayed(selectorAutocompleteList);
            driver.FindElement(selectorInput).SendKeys(Keys.ArrowDown + Keys.Enter);
        }

        protected void WaitForElementDisplayed(By selector)
        {
            wait.IsDisplayed(selector);
        }

        protected void WaitForElementDisplayed(By selector, int number)
        {
            wait.IsDisplayed(selector, number);
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

        private void WaitForElementEnabled(By selector, int number)
        {
            wait.IsEnabled(selector, number);
        }

        public void WaitForElementClicable(By selector)
        {
            wait.IsClicable(selector);
        }

        protected string GetTextFromElement(By selector)
        {
            WaitForElementDisplayed(selector);
            return driver.FindElement(selector).Text;
        }

        protected string GetTextFromElement(By selector, int number)
        {
            WaitForElementDisplayed(selector, number);
            return driver.FindElements(selector)[number].Text;
        }

        protected void IsEqualElements(string firstTextElement, string secondTextElement)
        {
            Assert.AreEqual(firstTextElement, secondTextElement);
        }

        protected void UncheckCheckbox(By selector)
        {
            if (driver.FindElement(selector).Selected)
            {
                IWebElement element = driver.FindElement(selector);
                Actions action = new Actions(driver);
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        protected void CheckCheckbox(By selector)
        {
            if (driver.FindElement(selector).Selected == false)
            {
                IWebElement element = driver.FindElement(selector);
                Actions action = new Actions(driver);
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
            if (!driver.FindElement(selector).Selected)
            {
                IWebElement element = driver.FindElement(selector);
                Actions action = new Actions(driver);
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        protected void ScrollPageDown(By selector)
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,750)", "");
            MoveToElement(selector);
            IWebElement element = driver.FindElement(selector);
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        protected string GetAttributeText(By selector, string text)
        {
            EnterText(selector, text);
            string value = driver.FindElement(selector).GetAttribute("value");
            driver.FindElement(selector).Clear();
            return value;
        }

        protected void SelectFromList(By selectorListButton, By selectorList, string text)
        {
            wait.WaitForAjaxToComplete();
            driver.FindElement(selectorListButton).Click();
            WaitForElementDisplayed(selectorList);
            driver.FindElement(selectorList).Click();
            //SelectElement select = new SelectElement(list);
            //select.SelectByValue(text);
        }

        private bool IsAlertPresent()
        {
            wait.WaitForAlert();
            IAlert alert = CustomExpectedConditions.AlertIsPresent().Invoke(driver);
            return (alert != null);
        }

        protected void CheckAlert()
        {
            if (IsAlertPresent())
            {
                wait.WaitForAlert();
                driver.SwitchTo().Alert().Accept();
            }
        }

        protected string GetUrl()
        {
            return driver.Url;
        }

        protected void GoBack()
        {
            driver.Navigate().Back();
        }

        protected void HoverElement(By selector, int number)
        {
            IWebElement element = driver.FindElements(selector)[number];
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        protected void HoverElement(By selector)
        {
            IWebElement element = driver.FindElement(selector);
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        protected void ClickOnHoverElement(By selector, int number)
        {
            HoverElement(selector, number);
            ClickOnElement(selector, number);
        }

        protected void ClickOnHoverElement(By selector)
        {
            HoverElement(selector);
            ClickOnElement(selector);
        }

        protected IList<string> GetElements(By selector)
        {
            IList<IWebElement> elements = driver.FindElements(selector);
            return elements.Select(e => e.Text).ToList();
        }

        protected bool CheckElementsList(string checkText, IList<string> elements)
        {
            bool isEquality = true; 
            foreach (var element in elements)
            {
                if (!element.Equals(checkText))
                {
                    isEquality = false;
                }
            }
            return isEquality;
        }

        protected void RefreshPage()
        {
            driver.Navigate().Refresh();
        }
    }
}
