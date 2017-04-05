using NUnit.Framework;
using Onliner_tests.PageObject.OrderPageObj;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using Onliner_tests.PageObject;

namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    class OrderTVOnlinerTest : BaseTastClass
    {
        private string _url = "https://catalog.onliner.by/tv";

        [Test]
        public void OrderPriceAscTVTest()
        {
            var resultComponent = new ResultComponent(webDriver);
            var basicOrderComponent = new BasicOrderComponent(webDriver);
            resultComponent.Open(_url);
            basicOrderComponent.ClickOrder(BasicOrderComponent.OrderType.PriceASC);
            resultComponent.ProcessingComplite();
            basicOrderComponent.ClickProductType(BasicOrderComponent.ProductType.New);
            try
            {
                resultComponent.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                resultComponent.ProcessingComplite();
            }
            double[] price = resultComponent.GetAllPriceOnThisPage();
            double[] priceSortASC = resultComponent.GetAllPriceOnThisPage();
            Array.Sort(priceSortASC);
            Assert.AreEqual(price, priceSortASC, "Error, wrong sorting of prices");
            log.Log(Status.Pass, "The order price by ASC works correctly");
        }

        [Test]
        public void OrderPriceDescTVTest()
        {
            var resultComponent = new ResultComponent(webDriver);
            var basicOrderComponent = new BasicOrderComponent(webDriver);
            resultComponent.Open(_url);
            basicOrderComponent.ClickProductType(BasicOrderComponent.ProductType.New);
            resultComponent.ProcessingComplite();
            basicOrderComponent.ClickOrder(BasicOrderComponent.OrderType.PriceDESC);
            try
            {
                resultComponent.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                resultComponent.ProcessingComplite();
            }
            double[] price = resultComponent.GetAllPriceOnThisPage();
            double[] priceSortDESC = resultComponent.GetAllPriceOnThisPage();
            Array.Sort(priceSortDESC);
            Array.Reverse(priceSortDESC);

            Assert.AreEqual(price, priceSortDESC, "Error, wrong sorting of prices");
            log.Log(Status.Pass, "The order price by DESC works correctly");
        }

        [Test]
        public void OrderRatingTVTest()
        {
            var resultComponent = new ResultComponent(webDriver);
            var basicOrderComponent = new BasicOrderComponent(webDriver);
            resultComponent.Open(_url);
            basicOrderComponent.ClickProductType(BasicOrderComponent.ProductType.New);
            resultComponent.ProcessingComplite();
            basicOrderComponent.ClickOrder(BasicOrderComponent.OrderType.Rating);
            try
            {
                resultComponent.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                resultComponent.ProcessingComplite();
            }
            double[] stars = resultComponent.GetAllStarsOnThisPage();
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

        [TestCaseSource(typeof(DataForTests), "DataTestOrderJsonForTV")]
        public void OrderOtherTVTest(BasicOrderComponent.OrderType type, string url)
        {
            var resultComponent = new ResultComponent(webDriver);
            var basicOrderComponent = new BasicOrderComponent(webDriver);
            resultComponent.Open(_url);
            basicOrderComponent.ClickProductType(BasicOrderComponent.ProductType.New);
            resultComponent.ProcessingComplite();
            basicOrderComponent.ClickOrder(type);
            try
            {
                resultComponent.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                resultComponent.ProcessingComplite();
            }
            List<string> fullNameListJSON = resultComponent.GetListJsonFullName(url);
            List<string> fullNameListPage = resultComponent.GetListFullnameOnThisPage();
            Assert.AreEqual(fullNameListJSON, fullNameListPage, "JSON is different");
            log.Log(Status.Pass, $"The order {type}  works correctly");
        }

    }
}
