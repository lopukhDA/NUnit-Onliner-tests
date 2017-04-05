using NUnit.Framework;
using Onliner_tests.PageObject.OrderPageObj;
using AventStack.ExtentReports;
using System;
using Onliner_tests.PageObject;
using System.Collections.Generic;

namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    class OrderNotebootOnlinerTests : BaseTastClass
    {
        private string _url = "https://catalog.onliner.by/notebook";

        [Test]
        public void OrderPriceAscNotebookTest()
        {
            var resultComponent = new ResultComponent(webDriver);
            var basicOrderComponent = new BasicOrderComponent(webDriver);
            resultComponent.Open(_url);
            basicOrderComponent.ClickProductType(BasicOrderComponent.ProductType.New);
            resultComponent.ProcessingComplite();
            basicOrderComponent.ClickOrder(BasicOrderComponent.OrderType.PriceASC);
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
        public void OrderPriceDescNotebookTest()
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
        public void OrderRaitingNotebookTest()
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

        [TestCaseSource(typeof(DataForTests), "DataTestOrderJsonForNotebook")]
        public void OrderOtherNotebookTest(BasicOrderComponent.OrderType type, string url)
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
