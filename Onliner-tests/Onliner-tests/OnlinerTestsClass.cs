using NUnit.Framework;
using System;
using System.Configuration;
using RelevantCodes.ExtentReports;

namespace Onliner_tests
{
    [TestFixture]
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
        
        [TestCase(300, 500)]
        public void SuccessfulPriceFilter(double min, double max)
        {
            log.StartTest($"SuccessfulPriceFilter({min}, {max})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
            catalogPage.Open();
            catalogPage.InputFilterMinPriceAndMaxPriceAndWaitComplitePrice(min, max);
            Assert.AreEqual($"{min} — {max}", _webDriver.GetText(catalogPage.FilterPrice), "Error, the entered filters do not match the received");
            log.Log(LogStatus.Pass, "The filter is correctly displayed on the page");
        }
        
        [Test]
        public void SuccessfulFilterForMinPrice([Random(300, 800, 3)] double m)
        {
            log.StartTest($"SuccessfulFilterForMinPrice({m})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
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
            Assert.IsFalse(error, "Error, found prices less than the minimum");
            log.Log(LogStatus.Pass, "The maximum filter works correctly");
        }

        [TestCaseSource(typeof(DataForTests), "DataTestMaxPrice")]
        public void SuccessfulFilterForMaxPrice(double max)
        {
            log.StartTest($"SuccessfulFilterForMaxPrice({max})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
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
            Assert.IsFalse(error, "Error, found prices exceeding the maximum ");
            log.Log(LogStatus.Pass, "The minimum filter works correctly");
        }

        [TestCase(300, 500)]
        public void SuccessfulFilterForMaxAndMinPrice(double min, double max)
        {
            log.StartTest($"SuccessfulFilterForMaxAndMinPrice({min}, {max})");
            var catalogPage = new PageObject.CatalogPage(_webDriver, log);
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
            Assert.IsFalse(error, "Error, found prices do not fall within the specified interval ");
            log.Log(LogStatus.Pass, "The interval filter works correctly");
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
