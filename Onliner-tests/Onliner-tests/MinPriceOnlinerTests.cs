using NUnit.Framework;
using System;
using RelevantCodes.ExtentReports;

namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    class MinPriceOnlinerTests : BaseTastClass
    {
        [TestCaseSource(typeof(DataForTests), "DataTestMaxPrice")]
        public void SuccessfulFilterForMaxPrice(double max)
        {
            log.StartTest($"SuccessfulFilterForMaxPrice({max})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            double maxPrice = max;
            catalogPage.InputFilterOnlyMaxPriceAndWaitComplitePrice(maxPrice);
            string[] price = catalogPage.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) > maxPrice)
                {
                    error = true;
                }
            }
            Assert.IsFalse(error, "Error, found prices exceeding the maximum ");
            log.Log(LogStatus.Pass, "The minimum filter works correctly");
        }
    }
}
