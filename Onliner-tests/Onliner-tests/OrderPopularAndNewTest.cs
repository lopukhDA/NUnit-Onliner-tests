using NUnit.Framework;
using System.Collections.Generic;


namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    class OrderPopularAndNewTest : BaseTastClass
    {
        [Test]
        public void FullNameFromJsonAndPage()
        {
            log.StartTest("FullNameFromJsonAndPage");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            catalogPage.ClickOrderNew();
            List<string> fullNameListJSON = catalogPage.GetListJSONfullName("https://catalog.api.onliner.by/search/notebook?group=0&order=date:desc");
            List<string> fullNameListPage = catalogPage.GetListPagefullName();
            Assert.AreEqual(fullNameListJSON, fullNameListPage, "JSON is different");
        }
    }
}
