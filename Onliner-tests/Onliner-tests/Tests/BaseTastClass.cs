using NUnit.Framework;
using System.Configuration;

namespace Onliner_tests.Tests
{
    public class BaseTastClass
    {
        protected WebDriver webDriver;
        protected LoggerClass log;

        [OneTimeSetUp]
        public void StartReport()
        {
            log = new LoggerClass();
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
            log.TearDown(webDriver.Driver);
            webDriver.Quit();
        }
  
    }
}
