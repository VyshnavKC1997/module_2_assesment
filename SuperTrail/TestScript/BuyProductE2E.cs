using BeatXP.Utilities;
using Serilog;
using SuperTrail.ExcelData;
using SuperTrail.Exceptions;
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
            test = extent.CreateTest("Search Product and Add to Cart Test");

            List<SearchAndBuyExcelData> exceldata=ExcelUtils.ReadExcelData(excelpath); //storing excel data into a list
            try
            {
                SuperTailHomePage homePage = new SuperTailHomePage(driver);
                homePage.CloseModalButtonClick();
                foreach (var excel in exceldata)
                {                 
                    var productPageObject=homePage.SearchBoxPassData(excel.Search);
                    Assert.That(driver.Url.Contains(excel.Search),"Search Product Step Failed");
                    Log.Information("Search Product Step Passed");
                    test.Info("search product step passed");
                    productPageObject.AddToCartProducts();
                    int cartCount = productPageObject.ProductsAddedToThECart();
                    Assert.That(cartCount, Is.GreaterThan(0),"Add To Cart step Failed");
                    Log.Information("Add To Cart step Passed");
                    test.AddScreenCaptureFromPath(TakeScreenShot(), "Add to cart test");
                    test.Info("Add to cart step passed");
//                    test.Pass();
                    var cartPageObject = productPageObject.ClickOnCartButton();
                    Assert.That(driver.Url.Contains("cart"), "Move to cart");
                    Log.Information("Moved to cart");
                    test.AddScreenCaptureFromPath(TakeScreenShot(), "Moved to cart");
                    test.Info("Move to cart step passed");
                    var paymentPageObject = cartPageObject.PlaceOrder();
                    Console.WriteLine(driver.Url);
                    Assert.That(driver.Url.Contains("cart"), "Move to CheckOut");

                    Log.Information("Moved to CheckOut");
                    test.AddScreenCaptureFromPath(TakeScreenShot(),"Moved to Checkout ");
                    test.Info("Moved to checkout");
                    var productDeliveryPageObject=paymentPageObject.FormOperations(excel.Email, excel.FirstName, excel.LastName, excel.Address, excel.Apartment, excel.State, excel.Pincode, excel.PhoneNumber,excel.City);
                    Assert.That(productDeliveryPageObject.IsSubmitbuttonAvailable(), "Payment page failed");
                    Log.Information("Payment page loaded successfully");
                    test.AddScreenCaptureFromPath(TakeScreenShot(), "Payment page loaded");

                    test.Info("Payment page loaded successfully");
                    test.Pass("Search Product and Add to Cart Test Passed");

                }
            }
            catch(SupertailException ex)
            {
                string message = ex.Message.Split(new[] { '\r', '\n' }).FirstOrDefault();
                Log.Error(message);
                //test = extent.CreateTest(message);
                test.Fail(message);
                
               
            }
        }
    }
}
