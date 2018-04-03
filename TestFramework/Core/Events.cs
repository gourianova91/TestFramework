using OpenQA.Selenium.Support.Events;

namespace TestFramework
{
    class Events
    {
        private Waiter wait = new Waiter();
  
        public void startEvents(EventFiringWebDriver eventHandler)
        {
            eventHandler.Navigated += Navigated;
            eventHandler.Navigating += Navigatin;
        }

        private void Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            wait.waitForAjaxToComplete();
            wait.waitForDocument();
        }

        private void Navigatin(object sender, WebDriverNavigationEventArgs e)
        {
            wait.waitForAjaxToComplete();
            wait.waitForDocument();
        }
    }
}
