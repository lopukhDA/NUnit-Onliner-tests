using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using NUnit.Framework.Interfaces;

namespace Onliner_tests
{
    [TestFixture]
    public class OnlinerTestsClass
    {
        WebDriver _webDriver;

        public ExtentReports extent;
        public ExtentTest test;

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
       
        [TestCaseSource(typeof(DataForTests), "DataTestAccount")]
        public void SuccessLogin(string login, string pass)
        {
            test = extent.StartTest("SuccessLogin");
            var loginPage = new PageObject.LoginPage(_webDriver, test);
            loginPage.Open();
            loginPage.Login(login, pass);
            var username = _webDriver.WaitElement(loginPage.Username);
            Assert.AreEqual("Dzmitry_Lopukh_test", _webDriver.GetText(username), "Username страницы отличается от ожидаемого");
            test.Log(LogStatus.Pass, "Successful login user");
        }
        
        [TestCase(300, 500)]
        public void SuccessfulPriceFilter(double min, double max)
        {
            test = extent.StartTest("SuccessfulPriceFilter");
            var catalogPage = new PageObject.CatalogPage(_webDriver, test);
            catalogPage.Open();
            catalogPage.InputFilterMinPriceAndMaxPriceAndWaitComplitePrice(min, max);
            Assert.AreEqual($"{min} — {max}", _webDriver.GetText(catalogPage.FilterPrice), "Ошибка, введенные фильтры не совпадают с полученным");
            test.Log(LogStatus.Pass, "The filter is correctly displayed on the page");
        }
        
        [Test]
        public void SuccessfulFilterForMinPrice([Random(300, 800, 3)] double m)
        {
            test = extent.StartTest("SuccessfulFilterForMinPrice");
            var catalogPage = new PageObject.CatalogPage(_webDriver, test);
            catalogPage.Open();
            double minPrice = m;
            catalogPage.InputFilterOnlyMinPriceAndWaitComplitePrice(minPrice);
            string[] price = catalogPage.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) < minPrice)
                {
                    error = true;

                }
            }
            Assert.IsFalse(error, "Ошибка, найдены цены меньше минимальной ");
            test.Log(LogStatus.Pass, "The maximum filter works correctly");
        }

        [TestCaseSource(typeof(DataForTests), "DataTestMaxPrice")]
        public void SuccessfulFilterForMaxPrice(double max)
        {
            test = extent.StartTest("SuccessfulFilterForMaxPrice");
            var catalogPage = new PageObject.CatalogPage(_webDriver, test);
            catalogPage.Open();
            double maxPrice = max;
            catalogPage.InputFilterOnlyMaxPriceAndWaitComplitePrice(maxPrice);
            string[] price = catalogPage.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) > maxPrice)
                {
                    error = true;
                }
            }
            Assert.IsFalse(error, "Ошибка, найдены цены превышающие максимальную ");
            test.Log(LogStatus.Pass, "The minimum filter works correctly");
        }

        [TestCase(300, 500)]
        public void SuccessfulFilterForMaxAndMinPrice(double min, double max)
        {
            test = extent.StartTest("SuccessfulFilterForMaxAndMinPrice");
            var catalogPage = new PageObject.CatalogPage(_webDriver, test);
            catalogPage.Open();
            double minPrice = min;
            double maxPrice = max;
            catalogPage.InputFilterFullPriceAndWaitComplitePrice(minPrice, maxPrice);
            string[] price = catalogPage.GetAllPriceInThisPage();
            bool error = false;
            for (int i = 0; i < price.Length; i++)
            {
                if (Convert.ToDouble(price[i]) > maxPrice && Convert.ToDouble(price[i]) < minPrice)
                {
                    error = true;
                }
            }
            Assert.IsFalse(error, "Ошибка, найдены цены не попадают в заданный промежуток ");
            test.Log(LogStatus.Pass, "The interval filter works correctly");
        }
        
    }

    class DataForTests
    {
        static object[] DataTestMaxPrice = {
            new object[] { 500 },
            new object[] { 1000 },
            new object[] { 800 }
        };
        static object[] DataTestAccount = {
            new object[] { ConfigurationManager.AppSettings.Get("Username"), ConfigurationManager.AppSettings.Get("Password") }
        };
    }
}
