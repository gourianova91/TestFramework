using NUnit.Framework;
using OpenQA.Selenium;

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

        public void clickOnElement(By selector)
        {
            waitForElementDisplayed(selector);
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
                driver.FindElement(selector).Click();
            }
        }

        public void checkCheckbox(By selector)
        {
            if (!driver.FindElement(selector).Selected)
            {
                driver.FindElement(selector).Click();
            }
        }
    }
}
