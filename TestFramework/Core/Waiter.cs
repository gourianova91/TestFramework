using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    public class Waiter
    {
        protected IWebDriver driver;
        private Stopwatch watch = new Stopwatch();
        public double MAX_WAIT = 15;                //Max wait time in seconds for element
        public double POLLING_INTERVAL = 500;       //Polling interval in milliseconds for element
        public int TIME_OUT = 60;                   //Timeout for ajax wait in seconds

        public Waiter()
        {
            driver = Driver.Instance.getWebDriver();
        }

        public void waitForAjaxToComplete()
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIME_OUT));
            wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }

        public void waitForDocument()
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
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
                    if (driver.FindElement(selector).Enabled)
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
            watch.Start();
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
                    if (driver.FindElement(selector).Displayed)
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
            watch.Start();
            return displayElement(selector);
        }
    }
}
