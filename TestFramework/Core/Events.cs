using OpenQA.Selenium.Support.Events;

namespace TestFramework.Core
{
    class Events
    {
        private readonly Waiter wait = new Waiter();
  
        public void StartEvents(EventFiringWebDriver eventHandler)
        {
            eventHandler.Navigated += Navigated;
            eventHandler.Navigating += Navigatin;
        }

        private void Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            wait.WaitForAjaxToComplete();
            wait.WaitForDocument();
        }

        private void Navigatin(object sender, WebDriverNavigationEventArgs e)
        {
            wait.WaitForAjaxToComplete();
            wait.WaitForDocument();
        }
    }
}
