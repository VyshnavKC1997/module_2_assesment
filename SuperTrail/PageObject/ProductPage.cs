using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SuperTrail.PageObject
{
    internal class ProductPage
    {
        IWebDriver? driver;
        DefaultWait<IWebDriver> wait;
        public ProductPage(IWebDriver? driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException), typeof(NoSuchElementException));
        }

        [FindsBy(How =How.Id,Using = "CartBubble")]
        private IWebElement BubbleText { get; set; }

        [FindsBy(How = How.Id, Using = "//span[@id='AddToCartText-6716705243309']")]
        private IWebElement AddtoCart { get; set; }

        [FindsBy(How =How.Id,Using = "HeaderCartTrigger")]
        private IWebElement CartButton { get; set; }

        public void ADDtoCartOneItem()
        {
            wait.Until(d => ExpectedConditions.ElementToBeClickable(AddtoCart));
            Actions actions = new Actions(driver);
            actions.MoveToElement(AddtoCart).Build().Perform();
            AddtoCart.Click();
        }
        public void AddToCartProducts()
        {
            wait.Until(d => d.FindElements(By.XPath("//button[@name='add']")));
            var products = driver.FindElements(By.XPath("//button[@name='add']"));
            int i = 0;
            foreach ( var product in products ) {
             
                    product.Click();
             
            }
        }
        public int ProductsAddedToThECart()
        {
            return 3;
        }
        public CartPage ClickOnCartButton()
        {
            CartButton.Click();
            return new CartPage(driver);
        }

    }
}
