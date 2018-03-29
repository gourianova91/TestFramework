using OpenQA.Selenium.Support.Events;

namespace TestFramework
{
    class Events
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
