using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace Onliner_tests.PageObject
{
    class CatalogPage
    {
        public CatalogPage(WebDriver driver, ExtentTest test)
        {
            _driver = driver;
            _test = test;
        }
        private WebDriver _driver;
        private ExtentTest _test;

        public void Open()
        {
            _driver.Navigate("https://catalog.onliner.by/notebook");
            _test.Log(LogStatus.Info, $"Go to page https://catalog.onliner.by/notebook");
        }

        public By MinPriceInput { get; set; } = By.XPath("//input[contains(@class, 'schema-filter__number-input_price') and contains(@data-bind,'value: facet.value.from')]");
        public By MaxPriceInput { get; set; } = By.XPath("//input[contains(@class, 'schema-filter__number-input_price') and contains(@data-bind,'value: facet.value.to')]");
        public By FilterPrice { get; set; } = By.CssSelector(".schema-tags__item span");
        public By Filter { get; set; } = By.ClassName("schema-filter-button__state_disabled");
        public By PriceProducts { get; set; } = By.CssSelector(".schema-product__price-value.schema-product__price-value_primary span");
        public By ProductCatalog { get; set; } = By.Id("schema-products");
        public By LoadingProduct { get; set; } = By.CssSelector(".schema-products");

        public void InputFilterMinPriceAndMaxPriceAndWaitComplitePrice(double minPrise, double maxPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MinPriceInput, minPrise.ToString());
            _test.Log(LogStatus.Info, $"Input minfilter price {minPrise.ToString()}");
            _driver.SendKeys(MaxPriceInput, maxPrise.ToString());
            _test.Log(LogStatus.Info, $"Input maxfilter price {maxPrise.ToString()}");
            _driver.WaitElement(FilterPrice);
        }

        public void InputFilterOnlyMinPriceAndWaitComplitePrice(double minPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MinPriceInput, minPrise.ToString());
            _test.Log(LogStatus.Info, $"Input minfilter price {minPrise.ToString()}");
            _driver.WaitElement(FilterPrice);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
            
        }

        public string[] GetAllPriceInThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(PriceProducts);
            String[] allPriceText = new String[allElements.Count];
            int i = 0;
            foreach (IWebElement element in allElements)
            {
                String[] price = element.GetAttribute("innerHTML").Split(",".ToCharArray());
                allPriceText[i++] = price[0];
            }
            return allPriceText;
        }

        public void InputFilterOnlyMaxPriceAndWaitComplitePrice(double maxPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MaxPriceInput, maxPrise.ToString());
            _test.Log(LogStatus.Info, $"Input maxfilter price {maxPrise.ToString()}");
            _driver.WaitElement(FilterPrice);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
        }

        public void InputFilterFullPriceAndWaitComplitePrice(double minPrise, double maxPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MinPriceInput, minPrise.ToString());
            _test.Log(LogStatus.Info, $"Input minfilter price {minPrise.ToString()}");
            _driver.SendKeys(MaxPriceInput, maxPrise.ToString());
            _test.Log(LogStatus.Info, $"Input maxfilter price {maxPrise.ToString()}");
            _driver.WaitElement(FilterPrice);
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
        }

    }
}
