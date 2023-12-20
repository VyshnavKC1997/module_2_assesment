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
    internal class PaymentPage
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;
        public PaymentPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException), typeof(NoSuchElementException));
        }

        [FindsBy(How =How.Id,Using = "checkout_email")]
        private IWebElement EmailInput { get; set; }

        [FindsBy(How = How.Id, Using = "checkout_shipping_address_first_name")]
        private IWebElement FirstName { get; set; }

        [FindsBy(How =How.Id,Using = "checkout_shipping_address_last_name")]
        private IWebElement LastName { get; set; }
        [FindsBy(How = How.Id, Using = "checkout_shipping_address_address1")]
        private IWebElement Address { get; set; }

        [FindsBy(How = How.Id, Using = "checkout_shipping_address_city")]
        private IWebElement City { get; set; }

        [FindsBy(How = How.Id, Using = "checkout_shipping_address_address2")]
        private IWebElement Apartment { get; set; }

    
        [FindsBy(How = How.Id, Using = "checkout_shipping_address_province")]
        private IWebElement SelectStateButton { get; set; }

        [FindsBy(How = How.Id, Using = "checkout_shipping_address_zip")]
        private IWebElement PinCodeInput { get; set; }
        
        [FindsBy(How = How.Id, Using = "checkout_shipping_address_phone")]
        private IWebElement PhoneNumber { get; set; }
        [FindsBy(How = How.Id, Using = "continue_button")]
        private IWebElement ContinueButton { get; set; }

        public ProductDeliveryPage FormOperations(string email,string firstname,string lastname,string address,string appartment,string
            state,string pincode,string phoneNumber,string city)
        {
            wait.Until(d => EmailInput.Displayed);
            EmailInput.SendKeys(email);
            FirstName.SendKeys(firstname);
            LastName.SendKeys(lastname);
            Address.SendKeys(address);
            Apartment.SendKeys(appartment);
            City.SendKeys(city);
            SelectStateButton.Click();
            driver.FindElement(By.XPath("//select[@placeholder='State']//option[@value='"+state+"']")).Click();
            PinCodeInput.SendKeys(pincode);
            PhoneNumber.SendKeys(phoneNumber);  
            ContinueButton.Click();
            return new ProductDeliveryPage(driver);

        }
        

        
    }
}
