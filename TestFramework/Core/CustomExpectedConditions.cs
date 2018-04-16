using System;
using OpenQA.Selenium;

namespace TestFramework.Core
{
    public static class CustomExpectedConditions
    {
        public static Func<IWebDriver, IAlert> AlertIsPresent()
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

        public static Func<IWebDriver, IWebElement> ElementToBeClickable(IWebElement element)
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
