using Onliner_tests.PageObject.FilterPageObj;
using Onliner_tests.PageObject.OrderPageObj;
using System.Configuration;


namespace Onliner_tests
{
    class DataForTests
    {
        static object[] DataTestMaxPrice = {
            new object[] { 400 }
        };

        static object[] DataTestAccount = {
            new object[] { ConfigurationManager.AppSettings.Get("Username"), ConfigurationManager.AppSettings.Get("Password") }
        };

        static object[] DataTestCPU = {
            new object[] { FilterNotebookPage.CpuType.AMDa10, "AMD A10" },
            new object[] { FilterNotebookPage.CpuType.AMDfx, "AMD FX" },
            new object[] { FilterNotebookPage.CpuType.IntelAtom, "Intel Atom" },
            new object[] { FilterNotebookPage.CpuType.IntelCoreI7, "Intel Core i7" },
            new object[] { FilterNotebookPage.CpuType.Samsung, "Samsung" }
        };

        static object[] DataTestOrderJsonForNotebook =
        {
            new object[] { BasicOrderPage.OrderType.New, "https://catalog.api.onliner.by/search/notebook?group=0&order=date:desc" },
            new object[] { BasicOrderPage.OrderType.Popular, "https://catalog.api.onliner.by/search/notebook?group=1&order=rating:desc" }
        };

        static object[] DataTestOrderJsonForTV =
        {
            new object[] { BasicOrderPage.OrderType.New, "https://catalog.api.onliner.by/search/tv?group=0&order=date:desc" },
            new object[] { BasicOrderPage.OrderType.Popular, "https://catalog.api.onliner.by/search/tv?group=1&order=rating:desc" }
        };

        static object[] DataTestOrderJsonForRefrigerator =
        {
            new object[] { BasicOrderPage.OrderType.New, "https://catalog.api.onliner.by/search/refrigerator?order=date:desc" },
            new object[] { BasicOrderPage.OrderType.Popular, "https://catalog.api.onliner.by/search/refrigerator?order=rating:desc" }
        };

    }
}
