using NUnit.Framework;
using Onliner_tests.PageObject.OrderPageObj;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;


namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    class OrderPopularAndNewNotebookOnlinerTest : BaseTastClass
    {
        private string _url = "https://catalog.onliner.by/notebook";

        [Test]
        public void SuccessfulNotebookOrderNew()
        {
            var catalogPage = new PageObject.CatalogPage(webDriver, log);
            var basicOrderPage = new BasicOrderPage(webDriver, log);
            catalogPage.Open(_url);
            basicOrderPage.ClickOrder(BasicOrderPage.OrderType.New);
            try
            {
                catalogPage.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                catalogPage.ProcessingComplite();
            }
            List<string> fullNameListJSON = catalogPage.GetListJsonFullName("https://catalog.api.onliner.by/search/notebook?group=0&order=date:desc");
            List<string> fullNameListPage = catalogPage.GetListPagefullName();
            Assert.AreEqual(fullNameListJSON, fullNameListPage, "JSON is different");
            log.Log(LogStatus.Pass, "The order new  works correctly");
        }

        [Test]
        public void SuccessfulNotebookOrderPopular()
        {
            var catalogPage = new PageObject.CatalogPage(webDriver, log);
            var basicOrderPage = new BasicOrderPage(webDriver, log);
            catalogPage.Open(_url);
            basicOrderPage.ClickOrder(BasicOrderPage.OrderType.Popular);
            try
            {
                catalogPage.WaitProcessing();
            }
            catch (Exception) { }
            finally
            {
                catalogPage.ProcessingComplite();
            }
            List<string> fullNameListJSON = catalogPage.GetListJsonFullName("https://catalog.api.onliner.by/search/notebook?group=1&order=rating:desc");
            List<string> fullNameListPage = catalogPage.GetListPagefullName();
            Assert.AreEqual(fullNameListJSON, fullNameListPage, "JSON is different");
            log.Log(LogStatus.Pass, "The order popular  works correctly");
        }
    }
}
