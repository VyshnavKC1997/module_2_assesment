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
    internal class SuperTailHomePage
    {
        IWebDriver? driver;
        DefaultWait<IWebDriver> wait;
        public SuperTailHomePage(IWebDriver? driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval=TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException),typeof(NoSuchElementException));
        }

        [FindsBy(How =How.ClassName,Using =("loginCross"))]
        private IWebElement CloseModalButton { get; set; }
       
        [FindsBy(How = How.Id, Using = ("mainfrm"))]
        private IWebElement SearchBox { get; set; }
        [FindsBy(How =How.XPath,Using =("//form[@role='search']//button[@type='submit']"))]
        private IWebElement SearchButton { get; set; }

        public void CloseModalButtonClick()
        {
            wait.Until(d => CloseModalButton.Displayed);
            CloseModalButton.Click();
        }
        public ProductPage SearchBoxPassData(string productName)
        {
            wait.Until(d => SearchBox.Displayed);
            SearchBox.SendKeys(productName);
            SearchButton.Click();
            return new ProductPage(driver);

        }
    }
}
