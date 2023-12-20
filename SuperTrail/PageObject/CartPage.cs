using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTrail.PageObject
{
    internal class CartPage
    {
        IWebDriver? driver;
        DefaultWait<IWebDriver> wait;
        public CartPage(IWebDriver? driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException), typeof(NoSuchElementException));
        }
        [FindsBy(How =How.XPath,Using = "//div[@class='cart__item-row cart__checkout-wrapper small--hide']//button[@name='checkout'][normalize-space()='Checkout']")]
        private IWebElement PlaceOrderButton { get; set; }

        [FindsBy(How=How.Id,Using = "savings_counter")]
        private IWebElement Amount {  get; set; }   

        public int ProductCount()
        {
            var remove = driver.FindElements(By.Id("minusqty"));
            return remove.Count();
        }
        public bool RemoveFromCartPage()
        {
            int counter = 0;
            var remove = driver.FindElements(By.Id("minusqty"));
            foreach(var item in remove)
            {
                wait.Until(d => ExpectedConditions.ElementToBeClickable(item));
                item.Click();
                counter++;
            }
            
            if (counter > 0)
                return true;
            return false;
        }
  
        public PaymentPage PlaceOrder()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", PlaceOrderButton);
            return new PaymentPage(driver);
        }
    }
}
