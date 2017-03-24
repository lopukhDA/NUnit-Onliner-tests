using NUnit.Framework;
using System;
using RelevantCodes.ExtentReports;

namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    class IntervalPriceOnlinerTests : BaseTastClass
    {

        [TestCase(300, 500)]
        public void SuccessfulFilterForMaxAndMinPrice(double min, double max)
        {
            log.StartTest($"SuccessfulFilterForMaxAndMinPrice({min}, {max})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            double minPrice = min;
            double maxPrice = max;
            catalogPage.InputFilterFullPriceAndWaitComplitePrice(minPrice, maxPrice);
            string[] price = catalogPage.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) > maxPrice && Convert.ToDouble(price[i]) < minPrice)
                {
                    error = true;
                }
            }
            Assert.IsFalse(error, "Error, found prices do not fall within the specified interval ");
            log.Log(LogStatus.Pass, "The interval filter works correctly");
        }
    }
}
