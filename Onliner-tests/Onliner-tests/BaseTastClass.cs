using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System.Configuration;

namespace Onliner_tests
{
    public class BaseTastClass
    {
        protected WebDriver _webDriver;
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
            _webDriver = new WebDriver(ConfigurationManager.AppSettings.Get("DriverType"), log);
        }

        [TearDown]
        public void TearDown()
        {
            log.TearDown();
            _webDriver.Quit();
        }
    }
}
