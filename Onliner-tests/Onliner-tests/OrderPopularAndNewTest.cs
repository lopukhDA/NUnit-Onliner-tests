using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System.Collections.Generic;


namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    class OrderPopularAndNewTest : BaseTastClass
    {
        [Test]
        public void SuccessfulOrderNew()
        {
            log.StartTest("SuccessfulOrderNew");
            var catalogPage = new PageObject.CatalogPage(webDriver, log);
            catalogPage.Open();
            catalogPage.ClickOrderNew();
            List<string> fullNameListJSON = catalogPage.GetListJsonFullName("https://catalog.api.onliner.by/search/notebook?group=0&order=date:desc");
            List<string> fullNameListPage = catalogPage.GetListPagefullName();
            Assert.AreEqual(fullNameListJSON, fullNameListPage, "JSON is different");
            log.Log(LogStatus.Pass, "The order new  works correctly");
        }

        [Test]
        public void SuccessfulOrderPopular()
        {
            log.StartTest("SuccessfulOrderPopular");
            var catalogPage = new PageObject.CatalogPage(webDriver, log);
            catalogPage.Open();
            catalogPage.ClickOrderPopular();
            List<string> fullNameListJSON = catalogPage.GetListJsonFullName("https://catalog.api.onliner.by/search/notebook?group=1&order=rating:desc");
            List<string> fullNameListPage = catalogPage.GetListPagefullName();
            Assert.AreEqual(fullNameListJSON, fullNameListPage, "JSON is different");
            log.Log(LogStatus.Pass, "The order popular  works correctly");
        }
    }
}
