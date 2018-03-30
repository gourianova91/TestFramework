using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    class BasePage
    {
        protected IWebDriver driver;
        Waiter wait = new Waiter();

        public BasePage()
        {
            driver = Driver.Instance.getWebDriver();
        }

        public void navigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void moveToElement(By selector)
        {
            var elem = driver.FindElement(selector);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", elem);
        }

        public void isClickable(By selector)
        {
            moveToElement(selector);
            waitForElementDisplayed(selector);
            waitForElementEnabled(selector);
        }

        public void clickOnElement(By selector)
        {
            isClickable(selector);
            driver.FindElement(selector).Click();
        }

        public void enterText(By selector, string text)
        {
            waitForElementDisplayed(selector);
            driver.FindElement(selector).Clear();
            driver.FindElement(selector).SendKeys(text);
        }

        public void waitForAutocompleteJquery(By selectorAutocompleteList, By selectorInput)
        {
            wait.isDisplayed(selectorAutocompleteList);
            driver.FindElement(selectorInput).SendKeys(Keys.ArrowDown + Keys.Enter);
        }

        public void waitForElementDisplayed(By selector)
        {
            wait.isDisplayed(selector);
        }

        public void waitForElementEnabled(By selector)
        {
            wait.isEnabled(selector);
        }

        public string getTextFromElement(By selector)
        {
            return driver.FindElement(selector).Text;
        }

        public void isEqualElements(string firstTextElement, string secondTextElement)
        {
            Assert.AreEqual(firstTextElement, secondTextElement);
        }

        public void uncheckCheckbox(By selector)
        {
            if (driver.FindElement(selector).Selected)
            {
                IWebElement element = driver.FindElement(selector);
                Actions action = new Actions(driver);
                action.MoveToElement(element).Click().Perform();
            }
        }

        public void checkCheckbox(By selector)
        {
            if (driver.FindElement(selector).Selected == false)
            {
                IWebElement element = driver.FindElement(selector);
                Actions action = new Actions(driver);
                action.MoveToElement(element).Click().Perform(); 
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
            if (!driver.FindElement(selector).Selected)
            {
                IWebElement element = driver.FindElement(selector);
                Actions action = new Actions(driver);
                action.MoveToElement(element).Click().Perform();
                //driver.FindElement(selector).Click();
            }
        }

        public void scrollPageDown()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,750)", "");
        }

        public string getSearchText(By selector, string text)
        {
            enterText(selector, text);
            return driver.FindElement(selector).GetAttribute("value");
        }

        public void selectFromList(By selectorListButton, By selectorList, string text)
        {
            wait.waitForAjaxToComplete();
            driver.FindElement(selectorListButton).Click();
            waitForElementDisplayed(selectorList);
            driver.FindElement(selectorList).Click();
            //SelectElement select = new SelectElement(list);
            //select.SelectByValue(text);
        }

    }
}
