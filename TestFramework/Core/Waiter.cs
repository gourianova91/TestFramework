using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.Core
{
    public class Waiter
    {
        protected IWebDriver driver;
        private readonly Stopwatch _watch = new Stopwatch();
        private const double MaxWait = 8; //Max wait time in seconds for element
        private const double PollingInterval = 500; //Polling interval in milliseconds for element
        private const int TimeOut = 60; //Timeout for ajax wait in seconds

        public Waiter()
        {
            driver = Driver.Instance.getWebDriver();
        }

        public void WaitForAjaxToComplete()
        {
            bool isJqueryPresent = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return (typeof jQuery != 'undefined');");
            if (isJqueryPresent)
            {
                IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeOut));
                wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
            }
        }

        public void WaitForDocument()
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
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
                    if (driver.FindElement(selector).Enabled)
                    {
                        _watch.Stop();
                        return true;
                    }
                    else
                    {
                        _watch.Stop();
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

        private bool EnableElement(By selector, int number)
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
                    if (driver.FindElements(selector)[number].Enabled)
                    {
                        _watch.Stop();
                        return true;
                    }
                    else
                    {
                        _watch.Stop();
                        return false;
                    }
                }
            }
            catch (Exception e) when (e is NoSuchElementException || e is StaleElementReferenceException)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(PollingInterval));
                return EnableElement(selector, number);
            }
        }

        public bool IsEnabled(By selector, int number)
        {
            _watch.Restart();
            return EnableElement(selector, number);
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
                    if (driver.FindElement(selector).Displayed)
                    {
                        _watch.Stop();
                        return true;
                    }
                    else
                    {
                        _watch.Stop();
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

        private bool DisplayElement(By selector, int number)
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
                    if (driver.FindElements(selector)[number].Displayed)
                    {
                        _watch.Stop();
                        return true;
                    }
                    else
                    {
                        _watch.Stop();
                        return false;
                    }
                }
            }
            catch (Exception e) when (e is NoSuchElementException || e is StaleElementReferenceException)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(PollingInterval));
                return DisplayElement(selector, number);
            }
        }

        public bool IsDisplayed(By selector, int number)
        {
            _watch.Restart();
            return DisplayElement(selector, number);
        }

        /*public bool isNotDisplayed(By selector)
        {

        }*/

        public void WaitForAlert()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
            wait.Until(CustomExpectedConditions.AlertIsPresent());
        }

        public void IsClicable(By selector)
        {
            IWebElement elem = driver.FindElement(selector);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15.00));
            wait.Until(CustomExpectedConditions.ElementToBeClickable(elem));
        }
    }
}
