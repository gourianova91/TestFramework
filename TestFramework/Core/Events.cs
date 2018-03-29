using OpenQA.Selenium.Support.Events;
using System.Threading;

namespace TestFramework
{
    class Events : Waiter
    {
        public void startEvents(EventFiringWebDriver eventHandler)
        {
            eventHandler.Navigated += navigated;
            eventHandler.Navigating += navigatin;
        }

        private void navigated(object sender, WebDriverNavigationEventArgs e)
        {
            
        }

        private void navigatin(object sender, WebDriverNavigationEventArgs e)
        {
            
        }
    }
}
