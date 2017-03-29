using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System;


namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    class OrderOnlinerTests : BaseTastClass
    {
        [Test]
        public void SuccessfulOrderPriceASC()
        {
            log.StartTest("SuccessfulOrderPriceASC");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            catalogPage.ClickOrderPriceASC();
            double[] price = catalogPage.GetAllPriceInThisPage();
            double[] priceSortASC = catalogPage.GetAllPriceInThisPage();
            Array.Sort(priceSortASC);
            Assert.AreEqual(price, priceSortASC, "Error, wrong sorting of prices");
            log.Log(LogStatus.Pass, "The order price by ASC works correctly");
        }

        [Test]
        public void SuccessfulOrderPriceDESC()
        {
            log.StartTest("SuccessfulOrderPriceDESC");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            catalogPage.ClickOrderPriceDESC();
            double[] price = catalogPage.GetAllPriceInThisPage();
            double[] priceSortDESC = catalogPage.GetAllPriceInThisPage();
            Array.Sort(priceSortDESC);
            Array.Reverse(priceSortDESC);
            
            Assert.AreEqual(price, priceSortDESC, "Error, wrong sorting of prices");
            log.Log(LogStatus.Pass, "The order price by DESC works correctly");
        }

        [Test]
        public void SuccessfulOrderRating()
        {
            log.StartTest("SuccessfulOrderRating");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            catalogPage.ClickOrderRating();
            int[] stars1 = catalogPage.GetAllStarsInThisPage();
            int[] stars = catalogPage.GetAllStarsInThisPage();
            for (int i=0; i<stars.Length - 1; i++)
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
