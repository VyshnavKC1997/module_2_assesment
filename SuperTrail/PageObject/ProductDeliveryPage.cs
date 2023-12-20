using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTrail.PageObject
{
    internal class ProductDeliveryPage
    {
        IWebDriver? driver;
        DefaultWait<IWebDriver> wait;
        public ProductDeliveryPage(IWebDriver? driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException), typeof(NoSuchElementException));
        }

        [FindsBy(How =How.Id,Using = "continue_button")]
        private IWebElement? ProceedButton { get; set; }


        public bool IsSubmitbuttonAvailable()
        {
            try
            {
                wait.Until(d => ProceedButton.Displayed);
                return true;
            }
            catch(WebDriverTimeoutException) { 
                return false;
            }
        }


    }
}
