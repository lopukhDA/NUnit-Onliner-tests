using NUnit.Framework;
using Onliner_tests.PageObject.FilterPageObj;
using Onliner_tests.PageObject.OrderPageObj;
using RelevantCodes.ExtentReports;
using System;


namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    class OrderNotebootOnlinerTests : BaseTastClass
    {
        private string _url = "https://catalog.onliner.by/notebook";

        [Test]
        public void SuccessfulNotebookOrderPriceASC()
        {
            var catalogPage = new PageObject.CatalogPage(webDriver, log);
            var basicOrderPage = new BasicOrderPage(webDriver, log);
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
            double[] priceSortASC = catalogPage.GetAllPriceInThisPage();
            Array.Sort(priceSortASC);
            Assert.AreEqual(price, priceSortASC, "Error, wrong sorting of prices");
            log.Log(LogStatus.Pass, "The order price by ASC works correctly");
        }

        [Test]
        public void SuccessfulNotebookOrderPriceDESC()
        {
            var catalogPage = new PageObject.CatalogPage(webDriver, log);
            var basicOrderPage = new BasicOrderPage(webDriver, log);
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
            double[] priceSortDESC = catalogPage.GetAllPriceInThisPage();
            Array.Sort(priceSortDESC);
            Array.Reverse(priceSortDESC);

            Assert.AreEqual(price, priceSortDESC, "Error, wrong sorting of prices");
            log.Log(LogStatus.Pass, "The order price by DESC works correctly");
        }

        [Test]
        public void SuccessfulNotebookOrderRating()
        {
            var catalogPage = new PageObject.CatalogPage(webDriver, log);
            var basicOrderPage = new BasicOrderPage(webDriver, log);
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
            int[] stars1 = catalogPage.GetAllStarsInThisPage();
            int[] stars = catalogPage.GetAllStarsInThisPage();
            for (int i = 0; i < stars.Length - 1; i++)
            {
                if (stars[i] < stars[i + 1])
                {
                    Assert.Fail("Error! The order rating not works");
                    break;
                }
            }

            log.Log(LogStatus.Pass, "The order rating  works correctly");
        }

        
    }
}
