using BeatXP.Utilities;
using NUnit.Framework;
using Serilog;
using SuperTrail.ExcelData;
using SuperTrail.PageObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTrail.TestScript
{
    internal class E2ECartTest:CoreCodes
    {
        [Test]
        public void CartTest()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            string excelpath = currDir + "/TestData/SuperTrail.xlsx";
            List<SearchAndBuyExcelData> exceldata = ExcelUtils.ReadExcelData(excelpath); //storing excel data into a list
            try
            {
                SuperTailHomePage homePage = new SuperTailHomePage(driver);
                homePage.CloseModalButtonClick();
                foreach (var excel in exceldata)
                {
                    var productPageObject = homePage.SearchBoxPassData(excel.Search);
                    Assert.That(driver.Url.Contains(excel.Search), "Search Product Test Failed");
                    Log.Information("Search Product Test Passed");
                    test = extent.CreateTest("Search Product Test");
                    test.Pass();
                    productPageObject.AddToCartProducts();
                    int cartCount = productPageObject.ProductsAddedToThECart();
                    Assert.That(cartCount, Is.GreaterThan(0), "Add To Cart Test Failed");
                    Log.Information("Add To Cart Test Passed");
                    test = extent.CreateTest("Search Product Test");
                    test.Pass();
                    var cartPageObject = productPageObject.ClickOnCartButton();
                    Assert.That(driver.Url.Contains("cart"), "Move to cart");
                    Log.Information("Move to cart Test Passed");
                    test = extent.CreateTest("Move To Cart  Test");
                    test.Pass();
                    int productInCart=cartPageObject.ProductCount();
                   
                    Assert.That(productInCart,Is.GreaterThan(0)," products are not added to cart");
                    Log.Information("Products Are Successfully Added to cart");
                    test = extent.CreateTest("Products Are Successfully Added to cart");
                    test.Pass();
                    bool isAmountZero=cartPageObject.RemoveFromCartPage();
                    Assert.That(isAmountZero, "Items Removed from cart");
                    Log.Information("Items Removed from cart");
                    test = extent.CreateTest("Items Removed from cart");
                    test.Pass();
                }

                }
            catch(AssertionException ex) 
            {
                string message = ex.Message.Split(new[] { '\r', '\n' }).FirstOrDefault();
                Log.Error(message);
                test = extent.CreateTest(message);
                test.Fail();
                TakeScreenShot();
            }
        }
    }
}
