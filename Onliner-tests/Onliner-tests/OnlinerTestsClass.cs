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

namespace Onliner_tests
{
    [TestFixture]
    public class OnlinerTestsClass
    {
        WebDriver _webDriver;

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
            _webDriver.Quit();
        }
       
        [TestCaseSource(typeof(DataForTests), "DataTestAccount")]
        public void SuccessLogin(string login, string pass)
        {
            var loginPage = new PageObject.LoginPage(_webDriver);
            loginPage.Open();
            loginPage.Login(login, pass);
            var username = _webDriver.WaitElement(loginPage.Username);
            Assert.AreEqual("Dzmitry_Lopukh_test", _webDriver.GetText(username), "Username страницы отличается от ожидаемого");
        }
        
        [TestCase(300, 500)]
        public void SuccessfulPriceFilter(double min, double max)
        {
            var catalogPage = new PageObject.CatalogPage(_webDriver);
            catalogPage.Open();
            catalogPage.InputFilterMinPriceAndMaxPriceAndWaitComplitePrice(min, max);
            Assert.AreEqual($"{min} — {max}", _webDriver.GetText(catalogPage.FilterPrice), "Ошибка, введенные фильтры не совпадают с полученным");
        }
        
        [Test]
        public void SuccessfulFilterForMinPrice([Random(300, 800, 3)] double m)
        {
            var catalogPage = new PageObject.CatalogPage(_webDriver);
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
        }

        [TestCaseSource(typeof(DataForTests), "DataTestMaxPrice")]
        public void SuccessfulFilterForMaxPrice(double max)
        {
            var catalogPage = new PageObject.CatalogPage(_webDriver);
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
        }

        [TestCase(300, 500)]
        public void SuccessfulFilterForMaxAndMinPrice(double min, double max)
        {
            var catalogPage = new PageObject.CatalogPage(_webDriver);
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
