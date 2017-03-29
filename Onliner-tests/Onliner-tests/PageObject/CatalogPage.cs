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
        public By SchemaFilter { get; set; } = By.CssSelector(".schema-filter-button__state_initial");
        public By SchemaFilterProcessing { get; set; } = By.CssSelector(".schema-products_processing");

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
            _driver.WaitElement(SchemaFilterProcessing);
            _driver.WaitElement(LoadingProductProcessing);

            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
            //_driver.WaitAllElNEW(RatingStar);
            _driver.WaitWhileElementClassContainsText(SchemaFilter, "schema-filter-button__state_animated");

            //Thread.Sleep(2000);
            
            //schema-filter-button__state schema-filter-button__state_initial schema-filter-button__state_disabled schema-filter-button__state_animated
            //schema-filter-button__state schema-filter-button__state_initial schema-filter-button__state_disabled
        }

        public int[] GetAllStarsInThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(By.CssSelector(".rating"));
            int[] allStarsText = new int[allElements.Count];
            int i = 0;
            foreach (IWebElement element in allElements)
            {
                String stars = element.GetAttribute("class").Replace("rating", "").Replace(" ", "").Replace("_", "").Replace(",", "");
                allStarsText[i++] = Convert.ToInt32(stars);
            }
            return allStarsText;
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

    }
}
