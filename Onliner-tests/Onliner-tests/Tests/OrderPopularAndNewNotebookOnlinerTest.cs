using NUnit.Framework;
using Onliner_tests.PageObject.OrderPageObj;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;


namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    class OrderPopularAndNewNotebookOnlinerTest : BaseTastClass
    {
        private string _url = "https://catalog.onliner.by/notebook";

        [TestCaseSource(typeof(DataForTests), "DataTestOrderJsonForNotebook")]
        public void SuccessfulNotebookOrderNew(BasicOrderPage.OrderType type, string url)
        {
            var catalogPage = new PageObject.CatalogPage(webDriver);
            var basicOrderPage = new BasicOrderPage(webDriver);
            catalogPage.Open(_url);
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

        //[Test]
        //public void SuccessfulNotebookOrderPopular()
        //{
        //    var catalogPage = new PageObject.CatalogPage(webDriver);
        //    var basicOrderPage = new BasicOrderPage(webDriver);
        //    catalogPage.Open(_url);
        //    basicOrderPage.ClickOrder(BasicOrderPage.OrderType.Popular);
        //    try
        //    {
        //        catalogPage.WaitProcessing();
        //    }
        //    catch (Exception) { }
        //    finally
        //    {
        //        catalogPage.ProcessingComplite();
        //    }
        //    List<string> fullNameListJSON = catalogPage.GetListJsonFullName("https://catalog.api.onliner.by/search/notebook?group=1&order=rating:desc");
        //    List<string> fullNameListPage = catalogPage.GetListPagefullName();
        //    Assert.AreEqual(fullNameListJSON, fullNameListPage, "JSON is different");
        //    log.Log(Status.Pass, "The order popular  works correctly");
        //}
    }
}
