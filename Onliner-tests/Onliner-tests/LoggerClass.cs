using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using System;
using System.Configuration;

namespace Onliner_tests
{
    public class LoggerClass
    {
        private ExtentReports _extent;
        private ExtentTest _test;
        public LoggerClass(ExtentReports extent, ExtentTest test)
        {
            _extent = extent;
            _test = test;
        }

        private static object _lock = new object();
        private static int _countReport = 0;

        public void OneSetUp()
        {
            lock (_lock)
            {
                _countReport++;
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string reportPath = projectPath + "Reports\\Report" + TestContext.CurrentContext.Test.FullName + ".html";

                _extent = new ExtentReports(reportPath, true);
                _extent
                    .AddSystemInfo("DriverType", ConfigurationManager.AppSettings.Get("DriverType"))
                    .AddSystemInfo("Using grid selenium", ConfigurationManager.AppSettings.Get("Grid"))
                    .AddSystemInfo("Autor", ConfigurationManager.AppSettings.Get("Autor"))
                    .AddSystemInfo("OSVersion", Environment.OSVersion.VersionString);
                if(ConfigurationManager.AppSettings.Get("Grid") == "true")
                {
                    _extent.AddSystemInfo("localhost", "http://" + ConfigurationManager.AppSettings.Get("localhost") + ":" + ConfigurationManager.AppSettings.Get("port") + "/wd/hub");
                    _extent.AddSystemInfo("Grid node PlatformType", ConfigurationManager.AppSettings.Get("PlatformType"));
                }
                _extent.LoadConfig(projectPath + "extent-config.xml");
            }
        }

        public void OneTearDown()
        {
            _extent.Flush();
            _extent.Close();
        }

        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                _test.Log(LogStatus.Fail, stackTrace + errorMessage);
            }
            _extent.EndTest(_test);
            _test.Log(LogStatus.Info, "EndTest() method will stop capturing information about the test log");
        }

        public void StartTest(string testname)
        {
            _test = _extent.StartTest(testname);
        }

        public void Log(LogStatus st, string text)
        {
            _test.Log(st, text);
        }

        public void Log(string text)
        {
            _test.Log(LogStatus.Info, text);
        }

        public void ErrorLog(string text)
        {
            _test.Log(LogStatus.Error, text);
        }

    }
}
