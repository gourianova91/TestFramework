using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    public class Waiter
    {
        //protected IWebDriver driver;
        private Stopwatch watch = new Stopwatch();
        public double MAX_WAIT = 8;                 //Max wait time in seconds for element
        public double POLLING_INTERVAL = 500;       //Polling interval in milliseconds for element
        public int TIME_OUT = 60;                   //Timeout for ajax wait in seconds

        public Waiter()
        {
            //driver = Driver.Instance.getWebDriver();
        }

        public void waitForAjaxToComplete()
        {
            bool is_jquery_present = (bool)(Driver.Instance.getWebDriver() as IJavaScriptExecutor).ExecuteScript("return (typeof jQuery != 'undefined');");
            if (is_jquery_present)
            {
                IWait<IWebDriver> wait = new WebDriverWait(Driver.Instance.getWebDriver(), TimeSpan.FromSeconds(TIME_OUT));
                wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
            }
        }

        public void waitForDocument()
        {
            IWait<IWebDriver> wait = new WebDriverWait(Driver.Instance.getWebDriver(), TimeSpan.FromSeconds(30.00));
            wait.Until(driver1 => ((IJavaScriptExecutor)Driver.Instance.getWebDriver()).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public bool enableElement(By selector)
        {
            try
            {
                if (watch.Elapsed >= TimeSpan.FromSeconds(MAX_WAIT))
                {
                    watch.Stop();
                    return false;
                }
                else
                {
                    if (Driver.Instance.getWebDriver().FindElement(selector).Enabled)
                    {
                        watch.Stop();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e) when (e is NoSuchElementException || e is StaleElementReferenceException)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(POLLING_INTERVAL));
                return enableElement(selector);
            }
        }

        public bool isEnabled(By selector)
        {
            watch.Restart();
            return enableElement(selector);
        }

        public bool displayElement(By selector)
        {
            try
            {
                if (watch.Elapsed >= TimeSpan.FromSeconds(MAX_WAIT))
                {
                    watch.Stop();
                    return false;
                }
                else
                {
                    if (Driver.Instance.getWebDriver().FindElement(selector).Displayed)
                    {
                        watch.Stop();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e) when (e is NoSuchElementException || e is StaleElementReferenceException)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(POLLING_INTERVAL));
                return displayElement(selector);
            }
        }

        public bool isDisplayed(By selector)
        {
            watch.Restart();
            return displayElement(selector);
        }

        /*public bool isNotDisplayed(By selector)
        {

        }*/

        public void waitForAlert()
        {
            WebDriverWait wait = new WebDriverWait(Driver.Instance.getWebDriver(), TimeSpan.FromSeconds(10.00));
            wait.Until(CustomExpectedConditions.alertIsPresent());
        }

        public void isClicable(By selector)
        {
            IWebElement elem = Driver.Instance.getWebDriver().FindElement(selector);
            WebDriverWait wait = new WebDriverWait(Driver.Instance.getWebDriver(), TimeSpan.FromSeconds(15.00));
            wait.Until(CustomExpectedConditions.elementToBeClickable(elem));
        }
    }
}
