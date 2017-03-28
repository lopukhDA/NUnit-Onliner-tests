using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System;


namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    class FilterOnlinerTests : BaseTastClass
    {
        [TestCaseSource(typeof(DataForTests), "DataTestMaxPrice")]
        public void SuccessfulFilterForMaxPrice(double max)
        {
            log.StartTest($"SuccessfulFilterForMaxPrice({max})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            double maxPrice = max;
            catalogPage.InputFilterOnlyMaxPriceAndWaitComplitePrice(maxPrice);
            double[] price = catalogPage.GetAllPriceInThisPage();
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

        [Test]
        public void SuccessfulFilterForMinPrice([Random(300, 800, 1)] double m)
        {
            log.StartTest($"SuccessfulFilterForMinPrice({m})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            double minPrice = m;
            catalogPage.InputFilterOnlyMinPriceAndWaitComplitePrice(minPrice);
            double[] price = catalogPage.GetAllPriceInThisPage();
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

        [TestCase(300, 500)]
        public void SuccessfulFilterForMaxAndMinPrice(double min, double max)
        {
            log.StartTest($"SuccessfulFilterForMaxAndMinPrice({min}, {max})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            double minPrice = min;
            double maxPrice = max;
            catalogPage.InputFilterFullPriceAndWaitComplitePrice(minPrice, maxPrice);
            double[] price = catalogPage.GetAllPriceInThisPage();
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
