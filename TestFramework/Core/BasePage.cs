using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    public class BasePage
    {
        //protected IWebDriver driver;
        private Waiter wait;

        public BasePage()
        {
            //driver = Driver.Instance.getWebDriver();
            wait = new Waiter();
        }

        public void navigateTo(string url)
        {
            Driver.Instance.getWebDriver().Navigate().GoToUrl(url);
            waitForAjax();
            waitForDocumentReady();
        }

        public void moveToElement(By selector)
        {
            var elem = Driver.Instance.getWebDriver().FindElement(selector);
            ((IJavaScriptExecutor)Driver.Instance.getWebDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
        }

        public void isClickable(By selector)
        {
            waitForElementDisplayed(selector);
            waitForElementEnabled(selector);
            moveToElement(selector);
        }

        public void clickOnElement(By selector)
        {
            isClickable(selector);
            Driver.Instance.getWebDriver().FindElement(selector).Click();
        }

        public void enterText(By selector, string text)
        {
            waitForElementDisplayed(selector);
            Driver.Instance.getWebDriver().FindElement(selector).Clear();
            Driver.Instance.getWebDriver().FindElement(selector).SendKeys(text);
        }

        public void waitForAutocompleteJquery(By selectorAutocompleteList, By selectorInput)
        {
            wait.isDisplayed(selectorAutocompleteList);
            Driver.Instance.getWebDriver().FindElement(selectorInput).SendKeys(Keys.ArrowDown + Keys.Enter);
        }

        public void waitForElementDisplayed(By selector)
        {
            wait.isDisplayed(selector);
        }

        public bool isElementDisplayed(By selector)
        {
            return wait.isDisplayed(selector);
        }

        //public bool isNotElementDisplayed(By selector)
        //{
        //    return wait.isNotDisplayed(selector);
        //}

        public void waitForElementEnabled(By selector)
        {
            wait.isEnabled(selector);
        }

        public void waitForElementClicable(By selector)
        {
            wait.isClicable(selector);
        }

        public string getTextFromElement(By selector)
        {
            waitForElementDisplayed(selector);
            return Driver.Instance.getWebDriver().FindElement(selector).Text;
        }

        public void isEqualElements(string firstTextElement, string secondTextElement)
        {
            Assert.AreEqual(firstTextElement, secondTextElement);
        }

        public void uncheckCheckbox(By selector)
        {
            if (Driver.Instance.getWebDriver().FindElement(selector).Selected)
            {
                IWebElement element = Driver.Instance.getWebDriver().FindElement(selector);
                Actions action = new Actions(Driver.Instance.getWebDriver());
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        public void checkCheckbox(By selector)
        {
            if (Driver.Instance.getWebDriver().FindElement(selector).Selected == false)
            {
                IWebElement element = Driver.Instance.getWebDriver().FindElement(selector);
                Actions action = new Actions(Driver.Instance.getWebDriver());
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        public void waitForAjax()
        {
            wait.waitForAjaxToComplete();
        }

        public void waitForDocumentReady()
        {
            wait.waitForDocument();
        }

        public void selectRadioButton(By selector)
        {
            if (!Driver.Instance.getWebDriver().FindElement(selector).Selected)
            {
                IWebElement element = Driver.Instance.getWebDriver().FindElement(selector);
                Actions action = new Actions(Driver.Instance.getWebDriver());
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        public void scrollPageDown(By selector)
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,750)", "");
            moveToElement(selector);
            IWebElement element = Driver.Instance.getWebDriver().FindElement(selector);
            Actions action = new Actions(Driver.Instance.getWebDriver());
            action.MoveToElement(element).Perform();
        }

        public string getAttributeText(By selector, string text)
        {
            enterText(selector, text);
            string value = Driver.Instance.getWebDriver().FindElement(selector).GetAttribute("value");
            Driver.Instance.getWebDriver().FindElement(selector).Clear();
            return value;
        }

        public void selectFromList(By selectorListButton, By selectorList, string text)
        {
            wait.waitForAjaxToComplete();
            Driver.Instance.getWebDriver().FindElement(selectorListButton).Click();
            waitForElementDisplayed(selectorList);
            Driver.Instance.getWebDriver().FindElement(selectorList).Click();
            //SelectElement select = new SelectElement(list);
            //select.SelectByValue(text);
        }

        public bool isAlertPresent()
        {
            wait.waitForAlert();
            IAlert alert = CustomExpectedConditions.alertIsPresent().Invoke(Driver.Instance.getWebDriver());
            return (alert != null);
        }

        public void checkAlert()
        {
            if (isAlertPresent())
            {
                wait.waitForAlert();
                Driver.Instance.getWebDriver().SwitchTo().Alert().Accept();
            }
        }

        public string getUrl()
        {
            return Driver.Instance.getWebDriver().Url;
        }

        public void goBack()
        {
            Driver.Instance.getWebDriver().Navigate().Back();
        }
    }
}
