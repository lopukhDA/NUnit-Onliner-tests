using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using System;
using System.Configuration;

namespace Onliner_tests
{
    public class BaseTastClass
    {
        protected WebDriver _webDriver;

        protected ExtentReports extent;
        protected ExtentTest test;

        [OneTimeSetUp]
        public void StartReport()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\MyOwnReport.html";

            extent = new ExtentReports(reportPath, true);
            extent.AddSystemInfo("Host Name", "Dzmitry").AddSystemInfo("User Name", "Dzmitry Lopukh");
            extent.LoadConfig(projectPath + "extent-config.xml");
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
            extent.Close();
        }

        [SetUp]
        public void Setup()
        {
            _webDriver = new WebDriver(ConfigurationManager.AppSettings.Get("DriverType"));
            
            //var loginPage = new PageObject.LoginPage(_webDriver);
            //loginPage.Open();
            //loginPage.Login("lopukh.d.a@yandex.ru", "testpassword");
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                test.Log(LogStatus.Fail, stackTrace + errorMessage);
            }
            extent.EndTest(test);
            test.Log(LogStatus.Info, "EndTest() method will stop capturing information about the test log");
            _webDriver.Quit();
        }
    }
}
