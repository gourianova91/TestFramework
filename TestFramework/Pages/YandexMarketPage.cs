using OpenQA.Selenium;

namespace TestFramework
{
    class YandexMarketPage : BasePage
    {
        public static By deliveryPrice = By.CssSelector("#delivery-included-filter");

        public void checkDeliveryPrice()
        {
            checkCheckbox(deliveryPrice);
        }
    }
}
