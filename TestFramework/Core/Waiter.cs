using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;

namespace TestFramework
{
    public class Waiter
    {
        protected IWebDriver driver;
        public double MAX_WAIT = 15;
        public double POLLING_INTERVAL = 500;

        public Waiter()
        {
            driver = Driver.Instance.getWebDriver();
        }

        Stopwatch watch = new Stopwatch();

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
                    return driver.FindElement(selector).Enabled;
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
                    return driver.FindElement(selector).Displayed;
                }
            }
            catch (Exception e) when (e is NoSuchElementException || e is StaleElementReferenceException)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(POLLING_INTERVAL));
                return enableElement(selector);
            }
        }

        public bool isDisplayed(By selector)
        {
            watch.Start();
            return displayElement(selector);
        }
    }
}
