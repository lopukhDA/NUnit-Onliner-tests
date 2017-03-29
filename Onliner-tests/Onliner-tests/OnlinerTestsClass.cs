using NUnit.Framework;
using System;
using System.Configuration;
using RelevantCodes.ExtentReports;

namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    public class OnlinerTestsClass : BaseTastClass
    {

        [TestCaseSource(typeof(DataForTests), "DataTestAccount")]
        public void SuccessLogin(string login, string pass)
        {
            log.StartTest($"SuccessLogin({login}, {pass})");
            var loginPage = new PageObject.LoginPage(_webDriver, log);
            loginPage.Open();
            loginPage.Login(login, pass);
            var username = _webDriver.WaitElement(loginPage.Username);
            Assert.AreEqual("Dzmitry_Lopukh_test", _webDriver.GetText(username), "The page's username is different than expected");
            log.Log(LogStatus.Pass, "Successful login user");
        }

        //[TestCase(300, 500)]
        //public void SuccessfulPriceFilter(double min, double max)
        //{
        //    log.StartTest($"SuccessfulPriceFilter({min}, {max})");
        //    var catalogPage = new PageObject.CatalogPage(_webDriver, log);
        //    catalogPage.Open();
        //    catalogPage.InputFilterMinPriceAndMaxPriceAndWaitComplitePrice(min, max);
        //    Assert.AreEqual($"{min} — {max}", _webDriver.GetText(catalogPage.FilterPrice), "Error, the entered filters do not match the received");
        //    log.Log(LogStatus.Pass, "The filter is correctly displayed on the page");
        //}

    }

    class DataForTests
    {

        static object[] DataTestMaxPrice = {
            new object[] { 500 }
        };

        static object[] DataTestAccount = {
            new object[] { ConfigurationManager.AppSettings.Get("Username"), ConfigurationManager.AppSettings.Get("Password") }
        };

    }
}
