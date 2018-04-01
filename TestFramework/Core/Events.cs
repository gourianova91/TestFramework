using OpenQA.Selenium.Support.Events;

namespace TestFramework
{
    class Events
    {
        public void startEvents(EventFiringWebDriver eventHandler)
        {
            eventHandler.Navigated += Navigated;
            eventHandler.Navigating += Navigatin;
        }

        private void Navigated(object sender, WebDriverNavigationEventArgs e)
        {

        }

        private void Navigatin(object sender, WebDriverNavigationEventArgs e)
        {

        }
    }
}
