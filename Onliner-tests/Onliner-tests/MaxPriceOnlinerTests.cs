using NUnit.Framework;
using System;
using RelevantCodes.ExtentReports;

namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    public class MaxPriceOnlinerTests : BaseTastClass
    {
        [Test]
        public void SuccessfulFilterForMinPrice([Random(300, 800, 3)] double m)
        {
            log.StartTest($"SuccessfulFilterForMinPrice({m})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            double minPrice = m;
            catalogPage.InputFilterOnlyMinPriceAndWaitComplitePrice(minPrice);
            string[] price = catalogPage.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) < minPrice)
                {
                    error = true;

                }
            }
            Assert.IsFalse(error, "Error, found prices less than the minimum");
            log.Log(LogStatus.Pass, "The maximum filter works correctly");
        }
    }
}
