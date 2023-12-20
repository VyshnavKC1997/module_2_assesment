using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SuperTailBDD.HooksAll;
using SuperTailBDD.utils;
using System;
using TechTalk.SpecFlow;

namespace SuperTailBDD.StepDefinitions
{
    [Binding]
    class CartFeatureStepDefinitions:CoreCodes
    {
        IWebDriver? driver = Hooks.driver;
        int count = 0;
        [Given(@"User will is on home page")]
        public void GivenUserWillIsOnHomePage()
        {
            driver.Url = "https://supertails.com/";

            

            DefaultWait<IWebDriver> wait;
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException), typeof(NoSuchElementException));
            wait.Until(d => SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("mainfrm"))));
                driver.FindElement(By.Id("mainfrm")).Click();

        }

        [When(@"User Type '([^']*)'")]
        public void WhenUserType(string pedigree)
        { 
            driver.FindElement(By.Id("mainfrm")).SendKeys(pedigree);
        }

        [When(@"click on serach button")]
        public void WhenClickOnSerachButton()
        {
            driver.FindElement(By.XPath("//form[@role='search']//button[@type='submit']")).Click();
        }

        [Then(@"User will be move into product page")]
        public void ThenUserWillBeMoveIntoProductPage()
        {
            DefaultWait<IWebDriver> wait;
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException), typeof(NoSuchElementException));
            wait.Until(d => SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("mainfrm"))));
            driver.FindElement(By.Id("mainfrm")).Click();
            try
            {
                Assert.That(driver.Url.Contains("pedigree"), "Search Product Test Failed");

                LogTestResult("search test", "passed", "failed");
                TakeScreenShot(driver);
            }
            catch (AssertionException ex)
            {
                LogTestResult("search test", "failed");
                TakeScreenShot(driver);
            }
        }

        [When(@"User click on add cart button for products")]
        public void WhenUserClickOnAddCartButtonForProducts()
        {
            var products = driver.FindElements(By.XPath("//button[@name='add']"));
            int i = 0;
            foreach (var product in products)
            {

                product.Click();

            }
        }

        [When(@"Click On goto cart button")]
        public void WhenClickOnGotoCartButton()
        {
            driver.FindElement(By.Id("HeaderCartTrigger")).Click();
        }

        [Then(@"User Will Be On Cart Page")]
        public void ThenUserWillBeOnCartPage()
        {
            try
            {
                Assert.That(driver.Url.Contains("cart"), "Move to cart");


            }
            catch (AssertionException ex)
            {
                LogTestResult("Move To Cart", "failed", "failed");
                TakeScreenShot(driver);
            }
        }

        [When(@"user removes products from cart")]
        public void WhenUserRemovesProductsFromCart()
        {
            var remove = driver.FindElements(By.Id("minusqty"));
            foreach (var item in remove)
            {
                DefaultWait<IWebDriver> wait;
                wait = new DefaultWait<IWebDriver>(driver);
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                wait.Timeout = TimeSpan.FromSeconds(10);
                wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException), typeof(NoSuchElementException));
                wait.Until(d => SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(item));
                item.Click();
                count++;
            }
        }

        [Then(@"products are removed")]
        public void ThenProductsAreRemoved()
        {
            try
            {
                Assert.That(count, Is.GreaterThan(0));
            }
            catch (AssertionException)
            {
                LogTestResult("Remove Cart", "failed", "failed");
                TakeScreenShot(driver);
            }
        }
    }
}
