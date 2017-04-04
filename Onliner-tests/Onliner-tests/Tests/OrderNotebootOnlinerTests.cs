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
            var catalogPage = new CatalogPage(webDriver);
            var basicOrderPage = new BasicOrderPage(webDriver);
            catalogPage.Open(_url);
            basicOrderPage.ClickProductType(BasicOrderPage.ProductType.New);
            catalogPage.ProcessingComplite();
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
            var catalogPage = new CatalogPage(webDriver);
            var basicOrderPage = new BasicOrderPage(webDriver);
            catalogPage.Open(_url);
            basicOrderPage.ClickProductType(BasicOrderPage.ProductType.New);
            catalogPage.ProcessingComplite();
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
            var catalogPage = new CatalogPage(webDriver);
            var basicOrderPage = new BasicOrderPage(webDriver);
            catalogPage.Open(_url);
            basicOrderPage.ClickProductType(BasicOrderPage.ProductType.New);
            catalogPage.ProcessingComplite();
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
            double[] stars = catalogPage.GetAllStarsInThisPage();
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
        public void OrderOtherNotebookTest(BasicOrderPage.OrderType type, string url)
        {
            var catalogPage = new CatalogPage(webDriver);
            var basicOrderPage = new BasicOrderPage(webDriver);
            catalogPage.Open(_url);
            basicOrderPage.ClickProductType(BasicOrderPage.ProductType.New);
            catalogPage.ProcessingComplite();
            basicOrderPage.ClickOrder(type);
            try
            {
                catalogPage.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                catalogPage.ProcessingComplite();
            }
            List<string> fullNameListJSON = catalogPage.GetListJsonFullName(url);
            List<string> fullNameListPage = catalogPage.GetListPagefullName();
            Assert.AreEqual(fullNameListJSON, fullNameListPage, "JSON is different");
            log.Log(Status.Pass, $"The order {type}  works correctly");
        }

    }
}
