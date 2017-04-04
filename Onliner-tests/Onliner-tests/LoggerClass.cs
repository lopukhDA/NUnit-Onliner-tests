﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.IO;

namespace Onliner_tests
{
    public class LoggerClass
    {
        private static ExtentReports _extent;
        private ExtentTest _test;
        private static int i = 1;
        private static string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
        private static string actualPath = path.Substring(0, path.LastIndexOf("bin"));
        private static string projectPath = new Uri(actualPath).LocalPath;
        private static string date = DateTime.Now.ToString("dd-MM-yyyy_HH-mm");

        static LoggerClass()
        {
            string reportPath = projectPath + "Reports\\" + date + "\\Report.html";
            _extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Configuration().ChartVisibilityOnOpen = false;
            htmlReporter.Configuration().DocumentTitle = "Onliner tests report";
            htmlReporter.Configuration().Theme = Theme.Dark;
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("DriverType", ConfigurationManager.AppSettings.Get("DriverType"));
            _extent.AddSystemInfo("Using grid selenium", ConfigurationManager.AppSettings.Get("Grid"));
            _extent.AddSystemInfo("Autor", ConfigurationManager.AppSettings.Get("Autor"));
            _extent.AddSystemInfo("OSVersion", Environment.OSVersion.VersionString);
            if (ConfigurationManager.AppSettings.Get("Grid") == "true")
            {
                _extent.AddSystemInfo("localhost", "http://" + ConfigurationManager.AppSettings.Get("localhost") + ":" + ConfigurationManager.AppSettings.Get("port") + "/wd/hub");
                _extent.AddSystemInfo("Grid node PlatformType", ConfigurationManager.AppSettings.Get("PlatformType"));
            }
        }

        public void OneTearDown()
        {
            _extent.Flush();
        }

        public void TearDown(IWebDriver driver)
        {
            Directory.CreateDirectory(projectPath + "Reports\\" + date);
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var message = TestContext.CurrentContext.Result.Message;
            if (status == TestStatus.Failed)
            {
                string imageFilePath = projectPath + "Reports\\" + date + $"\\scrin{i++}.png";
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(imageFilePath, ScreenshotImageFormat.Png);
                _test.Fail(stackTrace + message, MediaEntityBuilder.CreateScreenCaptureFromPath(imageFilePath).Build());
            }
            _test.Log(Status.Info, "EndTest() method will stop capturing information about the test log");
        }

        public void StartTest(string testname)
        {
            _test = _extent.CreateTest(testname);
        }

        public void Log(Status st, string text)
        {
            _test.Log(st, text);
        }

        public void Log(string text)
        {
            _test.Log(Status.Info, text);
        }

        public void Pass(string text)
        {
            _test.Pass(text);
        }

        public void Error(string text)
        {
            _test.Error(text);
        }

        public void Skip(string text)
        {
            _test.Skip(text);
        }

        public void Fail(string text)
        {
            _test.Fail(text);
        }

        public void Fatal(string text)
        {
            _test.Fatal(text);
        }

        public void ErrorLog(string text)
        {
            _test.Log(Status.Error, text);
        }

    }
}
