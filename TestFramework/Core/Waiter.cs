using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.Core
{
    public class Waiter
    {
        //protected IWebDriver driver;
        private readonly Stopwatch _watch = new Stopwatch();
        private const double MaxWait = 8; //Max wait time in seconds for element
        private const double PollingInterval = 500; //Polling interval in milliseconds for element
        private const int TimeOut = 60; //Timeout for ajax wait in seconds

        public Waiter()
        {
            //driver = Driver.Instance.getWebDriver();
        }

        public void WaitForAjaxToComplete()
        {
            bool isJqueryPresent = (bool)(Driver.Instance.getWebDriver() as IJavaScriptExecutor).ExecuteScript("return (typeof jQuery != 'undefined');");
            if (isJqueryPresent)
            {
                IWait<IWebDriver> wait = new WebDriverWait(Driver.Instance.getWebDriver(), TimeSpan.FromSeconds(TimeOut));
                wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
            }
        }

        public void WaitForDocument()
        {
            IWait<IWebDriver> wait = new WebDriverWait(Driver.Instance.getWebDriver(), TimeSpan.FromSeconds(30.00));
            wait.Until(driver1 => ((IJavaScriptExecutor)Driver.Instance.getWebDriver()).ExecuteScript("return document.readyState").Equals("complete"));
        }

        private bool EnableElement(By selector)
        {
            try
            {
                if (_watch.Elapsed >= TimeSpan.FromSeconds(MaxWait))
                {
                    _watch.Stop();
                    return false;
                }
                else
                {
                    if (Driver.Instance.getWebDriver().FindElement(selector).Enabled)
                    {
                        _watch.Stop();
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
                Thread.Sleep(TimeSpan.FromMilliseconds(PollingInterval));
                return EnableElement(selector);
            }
        }

        public bool IsEnabled(By selector)
        {
            _watch.Restart();
            return EnableElement(selector);
        }

        private bool DisplayElement(By selector)
        {
            try
            {
                if (_watch.Elapsed >= TimeSpan.FromSeconds(MaxWait))
                {
                    _watch.Stop();
                    return false;
                }
                else
                {
                    if (Driver.Instance.getWebDriver().FindElement(selector).Displayed)
                    {
                        _watch.Stop();
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
                Thread.Sleep(TimeSpan.FromMilliseconds(PollingInterval));
                return DisplayElement(selector);
            }
        }

        public bool IsDisplayed(By selector)
        {
            _watch.Restart();
            return DisplayElement(selector);
        }

        /*public bool isNotDisplayed(By selector)
        {

        }*/

        public void WaitForAlert()
        {
            WebDriverWait wait = new WebDriverWait(Driver.Instance.getWebDriver(), TimeSpan.FromSeconds(10.00));
            wait.Until(CustomExpectedConditions.AlertIsPresent());
        }

        public void IsClicable(By selector)
        {
            IWebElement elem = Driver.Instance.getWebDriver().FindElement(selector);
            WebDriverWait wait = new WebDriverWait(Driver.Instance.getWebDriver(), TimeSpan.FromSeconds(15.00));
            wait.Until(CustomExpectedConditions.ElementToBeClickable(elem));
        }
    }
}
