using NUnit.Framework;
using Onliner_tests.PageObject.FilterPageObj;
using Onliner_tests.PageObject;
using System;
using AventStack.ExtentReports;

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
            var resultComponent = new ResultComponent(webDriver);
            var filterNotebookComponent = new FilterNotebookComponent(webDriver);
            resultComponent.Open(_url);
            double maxPrice = max;
            filterNotebookComponent.InputFilterMaxPrice(maxPrice);
            resultComponent.ProcessingComplite();
            double[] price = resultComponent.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) > maxPrice)
                {
                    error = true;
                }
            }
            Assert.IsFalse(error, "Error, found prices exceeding the maximum ");
            log.Log(Status.Pass, "The minimum filter works correctly");
        }

        [Test]
        public void SuccessfulFilterNotebookForMinPrice([Random(300, 800, 1)] double m)
        {
            var resultComponent = new ResultComponent(webDriver);
            var filterNotebookComponent = new FilterNotebookComponent(webDriver);
            resultComponent.Open(_url);
            double minPrice = m;
            filterNotebookComponent.InputFilterMinPrice(minPrice);
            resultComponent.ProcessingComplite();
            double[] price = resultComponent.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) < minPrice)
                {
                    error = true;

                }
            }
            Assert.IsFalse(error, "Error, found prices less than the minimum");
            log.Log(Status.Pass, "The maximum filter works correctly");
        }

        [TestCase(300, 500)]
        public void SuccessfulFilterNotebookForMaxAndMinPrice(double min, double max)
        {
            var resultComponent = new ResultComponent(webDriver);
            var filterNotebookComponent = new FilterNotebookComponent(webDriver);
            resultComponent.Open(_url);
            double minPrice = min;
            double maxPrice = max;
            filterNotebookComponent.InputFilterFullPrice(minPrice, maxPrice);
            resultComponent.ProcessingComplite();
            double[] price = resultComponent.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) > maxPrice && Convert.ToDouble(price[i]) < minPrice)
                {
                    error = true;
                }
            }
            Assert.IsFalse(error, "Error, found prices do not fall within the specified interval ");
            log.Log(Status.Pass, "The interval filter works correctly");
        }

        [TestCaseSource(typeof(DataForTests), "DataTestCPU")]
        public void ProcessorFilterAMDa10Notebook(FilterNotebookComponent.CpuType type, string text)
        {
            var resultComponent = new ResultComponent(webDriver);
            var filterNotebookComponent = new FilterNotebookComponent(webDriver);
            resultComponent.Open(_url);
            filterNotebookComponent.SelectCPU(type);
            try
            {
                resultComponent.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                resultComponent.ProcessingComplite();
            }
            string[] descriptionAll = resultComponent.GetAllDescriptioninThePage();
            foreach (var item in descriptionAll)
            {
                if (!item.Contains(text))
                {
                    Assert.Fail($"The processor '{text}' filter not works correctly");
                }
            }
            log.Log(Status.Pass, $"The processor {text} filter works correctly");
        }

    }
}
