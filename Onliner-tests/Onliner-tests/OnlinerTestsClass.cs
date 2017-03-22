using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
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
            _webDriver = new WebDriver();
            //var loginPage = new PageObject.LoginPage(_webDriver);
            //loginPage.Open();
            //loginPage.Login("lopukh.d.a@mail.ru", "Dima4862");
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        /*
         [Test]
         public void SuccessLogin()
         {
             var loginPage = new PageObject.LoginPage(_webDriver);
             loginPage.Open();
             loginPage.Login("lopukh.d.a@mail.ru", "Dima4862");
             var username = _webDriver.WaitElement(loginPage.Username);
             Assert.AreEqual("Dzmitry_Lopukh", _webDriver.GetText(username), "Username страницы отличается от ожидаемого");
         }

         [Test]
         public void SuccessfulPriceFilter()
         {
             var catalogPage = new PageObject.CatalogPage(_webDriver);
             catalogPage.Open();
             catalogPage.InputFilterMinPriceAndMaxPriceAndWaitComplitePrice(300, 500);
             Assert.AreEqual("300 — 500", _webDriver.GetText(catalogPage.FilterPrice), "Ошибка, введенные фильтры не совпадают с полученными");
         }
         */
        [Test]
        public void SuccessfulFilterForMinPrice()
        {
            var catalogPage = new PageObject.CatalogPage(_webDriver);
            catalogPage.Open();
            double minPrice = 300;
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
        
        [Test]
        public void SuccessfulFilterForMaxPrice()
        {
            var catalogPage = new PageObject.CatalogPage(_webDriver);
            catalogPage.Open();
            double maxPrice = 800;
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
       
        [Test]
        public void SuccessfulFilterForMaxAndMinPrice()
        {
            var catalogPage = new PageObject.CatalogPage(_webDriver);
            catalogPage.Open();
            double minPrice = 300;
            double maxPrice = 800;
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
}
