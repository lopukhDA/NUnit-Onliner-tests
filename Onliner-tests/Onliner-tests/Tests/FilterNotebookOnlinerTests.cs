using NUnit.Framework;
using Onliner_tests.PageObject.FilterPageObj;
using Onliner_tests.PageObject;
using RelevantCodes.ExtentReports;
using System;


namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    class FilterNotebookOnlinerTests : BaseTastClass
    {
        private string _url = "https://catalog.onliner.by/notebook";

        [TestCaseSource(typeof(DataForTests), "DataTestMaxPrice")]
        public void SuccessfulFilterNotebookForMaxPrice(double max)
        {
            var catalogPage = new CatalogPage(webDriver, log);
            var filterNotebookPage = new FilterNotebookPage(webDriver, log);
            catalogPage.Open(_url);
            double maxPrice = max;
            filterNotebookPage.InputFilterMaxPrice(maxPrice);
            catalogPage.ProcessingComplite();
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
        public void SuccessfulFilterNotebookForMinPrice([Random(300, 800, 1)] double m)
        {
            var catalogPage = new CatalogPage(webDriver, log);
            var basicFilterPage = new BasicFilterPage(webDriver, log);
            catalogPage.Open(_url);
            double minPrice = m;
            basicFilterPage.InputFilterMinPrice(minPrice);
            catalogPage.ProcessingComplite();
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
        public void SuccessfulFilterNotebookForMaxAndMinPrice(double min, double max)
        {
            var catalogPage = new CatalogPage(webDriver, log);
            var basicFilterPage = new BasicFilterPage(webDriver, log);
            catalogPage.Open(_url);
            double minPrice = min;
            double maxPrice = max;
            basicFilterPage.InputFilterFullPrice(minPrice, maxPrice);
            catalogPage.ProcessingComplite();
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

        //[Test]
        [TestCaseSource(typeof(DataForTests), "DataTestCPU")]
        public void ProcessorFilterAMDa10Notebook(FilterNotebookPage.CpuType type, string text)
        {
            var catalogPage = new CatalogPage(webDriver, log);
            var filterNotebookPage = new FilterNotebookPage(webDriver, log);
            catalogPage.Open(_url);
            filterNotebookPage.SelectCPU(type);
            try
            {
                catalogPage.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                catalogPage.ProcessingComplite();
            }
            string[] descriptionAll = catalogPage.GetAllDescriptioninThePage();
            foreach (var item in descriptionAll)
            {
                if(!item.Contains(text))
                {
                    Assert.Fail($"The processor '{text}' filter not works correctly");
                }
            }
            log.Log(LogStatus.Pass, $"The processor {text} filter works correctly");
        }

    }
}
