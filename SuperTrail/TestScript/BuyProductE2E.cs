using BeatXP.Utilities;
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
    internal class BuyProductE2E:CoreCodes
    {
        [Test]
        public void SearchAndBuyProduct()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            string excelpath = currDir + "/TestData/SuperTrail.xlsx";
            List<SearchAndBuyExcelData> exceldata=ExcelUtils.ReadExcelData(excelpath); //storing excel data into a list
            try
            {
                SuperTailHomePage homePage = new SuperTailHomePage(driver);
                homePage.CloseModalButtonClick();
                foreach (var excel in exceldata)
                {                 
                    var productPageObject=homePage.SearchBoxPassData(excel.Search);
                    Assert.That(driver.Url.Contains(excel.Search),"Search Product Test Failed");
                    Log.Information("Search Product Test Passed");
                    test = extent.CreateTest("Search Product Test");
                    test.Pass();
                    productPageObject.AddToCartProducts();
                    int cartCount = productPageObject.ProductsAddedToThECart();
                    Assert.That(cartCount, Is.GreaterThan(0),"Add To Cart Test Failed");
                    Log.Information("Add To Cart Test Passed");
                    test = extent.CreateTest("Search Product Test");
                    test.Pass();
                    var cartPageObject = productPageObject.ClickOnCartButton();
                    Assert.That(driver.Url.Contains("cart"), "Move to cart");
                    Log.Information("Move to cart Test Passed");
                    test = extent.CreateTest("Move To Cart  Test");
                    test.Pass();
                    var paymentPageObject = cartPageObject.PlaceOrder();
                    Console.WriteLine(driver.Url);
                    Assert.That(driver.Url.Contains("cart"), "Move to CheckOut");
                    
                    Log.Information("Move to CheckOut Test Passed");
                    test = extent.CreateTest("Move To CheckOut  Test");
                    test.Pass();
                    var productDeliveryPageObject=paymentPageObject.FormOperations(excel.Email, excel.FirstName, excel.LastName, excel.Address, excel.Apartment, excel.State, excel.Pincode, excel.PhoneNumber,excel.City);
                    Assert.That(productDeliveryPageObject.IsSubmitbuttonAvailable(), "Payment page failed");
                    Log.Information("Payment page loaded successfully");
                    test = extent.CreateTest("Payment page loaded successfully");
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
