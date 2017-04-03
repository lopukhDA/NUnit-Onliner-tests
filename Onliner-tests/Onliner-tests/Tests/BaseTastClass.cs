using NUnit.Framework;
using Onliner_tests.PageObject.FilterPageObj;
using RelevantCodes.ExtentReports;
using System.Configuration;

namespace Onliner_tests.Tests
{
    public class BaseTastClass
    {
        protected WebDriver webDriver;
        protected static ExtentReports extent;
        protected static ExtentTest test;

        protected LoggerClass log = new LoggerClass(extent, test);

        [OneTimeSetUp]
        public void StartReport()
        {
            log.OneSetUp();
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            log.OneTearDown();
        }

        [SetUp]
        public void Setup()
        {
            log.StartTest(TestContext.CurrentContext.Test.Name);
            webDriver = new WebDriver(ConfigurationManager.AppSettings.Get("DriverType"), log);
        }

        [TearDown]
        public void TearDown()
        {
            log.TearDown();
            webDriver.Quit();
        }
  
    }

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
    }

}
