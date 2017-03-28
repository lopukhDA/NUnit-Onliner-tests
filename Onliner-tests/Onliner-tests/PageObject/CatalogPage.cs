using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;

namespace Onliner_tests.PageObject
{
    class CatalogPage
    {
        private LoggerClass _log;
        private WebDriver _driver;

        public CatalogPage(WebDriver driver, LoggerClass log)
        {
            _driver = driver;
            _log = log;
        }
        
        public void Open()
        {
            _driver.Navigate("https://catalog.onliner.by/notebook");
        }

        public By MinPriceInput { get; set; } = By.XPath("//input[contains(@class, 'schema-filter__number-input_price') and contains(@data-bind,'value: facet.value.from')]");
        public By MaxPriceInput { get; set; } = By.XPath("//input[contains(@class, 'schema-filter__number-input_price') and contains(@data-bind,'value: facet.value.to')]");
        public By FilterPrice { get; set; } = By.CssSelector(".schema-tags__item span");
        public By Filter { get; set; } = By.ClassName("schema-filter-button__state_disabled");
        public By PriceProducts { get; set; } = By.CssSelector(".schema-product__price-value.schema-product__price-value_primary span");
        public By ProductCatalog { get; set; } = By.Id("schema-products");
        public By LoadingProduct { get; set; } = By.CssSelector(".schema-products");
        public By LoadingProductProcessing { get; set; } = By.CssSelector(".schema-products.schema-products_processing");
        public By ShowOrderLink { get; set; } = By.CssSelector(".schema-order__link");
        public By OrderPriceASC { get; set; } = By.CssSelector(".schema-order__item:nth-child(2)");
        public By OrderPriceDESC { get; set; } = By.CssSelector(".schema-order__item:nth-child(3)");
        public By OrderRating { get; set; } = By.CssSelector(".schema-order__item:nth-child(5)");
        public By RatingStar { get; set; } = By.CssSelector(".rating");

        public void InputFilterMinPriceAndMaxPriceAndWaitComplitePrice(double minPrise, double maxPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MinPriceInput, minPrise.ToString());
            _driver.SendKeys(MaxPriceInput, maxPrise.ToString());
            _driver.WaitElement(FilterPrice);
        }

        public void InputFilterOnlyMinPriceAndWaitComplitePrice(double minPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MinPriceInput, minPrise.ToString());
            _driver.WaitElement(FilterPrice);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
            
        }

        public double[] GetAllPriceInThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(PriceProducts);
            double[] allPriceText = new double[allElements.Count];
            int i = 0;
            foreach (IWebElement element in allElements)
            {
                String price = element.GetAttribute("innerHTML").Replace("&nbsp;", "").Replace("р.", "").Replace(",", ".");
                allPriceText[i++] = Convert.ToDouble(price);
            }
            return allPriceText;
        }

        public void InputFilterOnlyMaxPriceAndWaitComplitePrice(double maxPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MaxPriceInput, maxPrise.ToString());
            _driver.WaitElement(FilterPrice);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
        }

        public void InputFilterFullPriceAndWaitComplitePrice(double minPrise, double maxPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MinPriceInput, minPrise.ToString());
            _driver.SendKeys(MaxPriceInput, maxPrise.ToString());
            _driver.WaitElement(FilterPrice);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
        }

        public void ClickOrderPriceASC()
        {
            _driver.WaitElement(Filter);
            _driver.Click(ShowOrderLink);
            _driver.Click(OrderPriceASC);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
            _driver.WaitElementAll(PriceProducts); 
        }

        public void ClickOrderPriceDESC()
        {
            _driver.WaitElement(Filter);
            _driver.Click(ShowOrderLink);
            _driver.Click(OrderPriceDESC);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
            _driver.WaitElementAll(PriceProducts);
        }

        public void ClickOrderRating()
        {
            _driver.WaitElement(Filter);
            _driver.Click(ShowOrderLink);
            _driver.Click(OrderRating);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
            Thread.Sleep(1000);
            _driver.WaitElementAll(RatingStar);
        }

        public int[] GetAllStarsInThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(RatingStar);
            int[] allPriceText = new int[allElements.Count];
            int i = 0;
            foreach (IWebElement element in allElements)
            {
                String stars = element.GetAttribute("class").Replace("rating", "").Replace(" ", "").Replace("_", "").Replace(",", "");
                allPriceText[i++] = Convert.ToInt32(stars);
            }
            //return allPriceText;


            //int[] a = new int[10];
            return allPriceText;
        }

    }
}
