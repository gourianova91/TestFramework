using System;
using OpenQA.Selenium;

namespace TestFramework
{
    public class CustomExpectedConditions
    {
        //protected IWebDriver driver;

        public CustomExpectedConditions()
        {
            //driver = Driver.Instance.getWebDriver();
        }

        public static Func<IWebDriver, IAlert> alertIsPresent()
        {
            return (driver) =>
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, IWebElement> elementToBeClickable(IWebElement element)
        { 
            return (driver) =>
            {
                try
                {
                    if (/*element != null && */element.Displayed/* && element.Enabled*/)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }
    }
}
