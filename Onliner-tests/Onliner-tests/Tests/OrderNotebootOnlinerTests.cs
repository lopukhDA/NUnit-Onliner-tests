using NUnit.Framework;
using Onliner_tests.PageObject.OrderPageObj;
using AventStack.ExtentReports;
using System;
using Onliner_tests.PageObject;

namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    class OrderNotebootOnlinerTests : BaseTastClass
    {
        private string _url = "https://catalog.onliner.by/notebook";

        //[Test]
        //public void SuccessfulNotebookOrderPriceASC()
        //{
        //    var catalogPage = new PageObject.CatalogPage(webDriver);
        //    var basicOrderPage = new BasicOrderPage(webDriver);
        //    catalogPage.Open(_url);
        //    basicOrderPage.ClickOrder(BasicOrderPage.OrderType.PriceASC);
        //    try
        //    {
        //        catalogPage.WaitProcessing();
        //    }
        //    catch (Exception) { }
        //    finally
        //    {
        //        catalogPage.ProcessingComplite();
        //    }
        //    double[] price = catalogPage.GetAllPriceInThisPage();
        //    for (int i = 0; i < price.Length - 1; i++)
        //    {
        //        if (price[i] > price[i + 1])
        //        {
        //            Assert.Fail("Error! The order price by ASC not works");
        //            break;
        //        }
        //    }
        //    log.Log(Status.Pass, "The order price by ASC works correctly");
        //}


        [Test]
        public void SuccessfulNotebookOrderPriceASC()
        {
            var catalogPage = new CatalogPage(webDriver);
            var basicOrderPage = new BasicOrderPage(webDriver);
            catalogPage.Open(_url);
            basicOrderPage.ClickOrder(BasicOrderPage.OrderType.PriceASC);
            try
            {
                catalogPage.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                catalogPage.ProcessingComplite();
            }
            double[] price = catalogPage.GetAllPriceInThisPage();
            for (int i = 0; i < price.Length - 1; i++)
            {
                if (price[i] > price[i + 1])
                {
                    Assert.Fail("Error! The order price by ASC not works");
                    break;
                }
            }
            log.Log(Status.Pass, "The order price by ASC works correctly");
        }

        [Test]
        public void SuccessfulNotebookOrderPriceDESC()
        {
            var catalogPage = new CatalogPage(webDriver);
            var basicOrderPage = new BasicOrderPage(webDriver);
            catalogPage.Open(_url);
            basicOrderPage.ClickOrder(BasicOrderPage.OrderType.PriceDESC);
            try
            {
                catalogPage.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                catalogPage.ProcessingComplite();
            }
            double[] price = catalogPage.GetAllPriceInThisPage();
            for (int i = 0; i < price.Length - 1; i++)
            {
                if (price[i] < price[i + 1])
                {
                    Assert.Fail("Error! The order price by DESC not works");
                    break;
                }
            }

            log.Log(Status.Pass, "The order price by DESC works correctly");
        }

        [Test]
        public void SuccessfulNotebookOrderRating()
        {
            var catalogPage = new CatalogPage(webDriver);
            var basicOrderPage = new BasicOrderPage(webDriver);
            catalogPage.Open(_url);
            basicOrderPage.ClickOrder(BasicOrderPage.OrderType.Rating);
            try
            {
                catalogPage.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                catalogPage.ProcessingComplite();
            }
            double[] stars = catalogPage.GetAllStarsInThisPage();
            for (int i = 0; i < stars.Length - 1; i++)
            {
                if (stars[i] < stars[i + 1])
                {
                    Assert.Fail("Error! The order rating not works");
                    break;
                }
            }

            log.Log(Status.Pass, "The order rating  works correctly");
        }

        
    }
}
