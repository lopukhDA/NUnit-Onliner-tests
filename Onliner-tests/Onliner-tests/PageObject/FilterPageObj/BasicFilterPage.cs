﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onliner_tests.PageObject.FilterPageObj
{
    public class BasicFilterPage
    {
        private LoggerClass _log;
        private WebDriver _driver;

        public BasicFilterPage(WebDriver driver, LoggerClass log)
        {
            _driver = driver;
            _log = log;
        }

        public By MinPriceInput { get; set; } = By.XPath("//input[contains(@class, 'schema-filter__number-input_price') and contains(@data-bind,'value: facet.value.from')]");
        public By MaxPriceInput { get; set; } = By.XPath("//input[contains(@class, 'schema-filter__number-input_price') and contains(@data-bind,'value: facet.value.to')]");
        public By FilterPrice { get; set; } = By.CssSelector(".schema-tags__item span");
        public By Filter { get; set; } = By.ClassName("schema-filter-button__state_disabled");
        public By SchemaFilter { get; set; } = By.CssSelector(".schema-filter-button__state_initial");
        public By SchemaFilterProcessing { get; set; } = By.CssSelector(".schema-products_processing");

        public void InputFilterMinPrice(double minPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MinPriceInput, minPrise.ToString());;
            WaitComplitePrice();
        }

        public void InputFilterMaxPrice(double maxPrise)
        {
            _driver.WaitElement(Filter);
            _driver.SendKeys(MaxPriceInput, maxPrise.ToString());
            WaitComplitePrice();
        }

        public void InputFilterMinPriceAndMaxPrice(double minPrise, double maxPrise)
        {
            InputFilterMinPrice(minPrise);
            InputFilterMaxPrice(maxPrise);
        }

        public void InputFilterFullPrice(double minPrise, double maxPrise)
        {
            InputFilterMinPrice(minPrise);
            InputFilterMaxPrice(maxPrise);
        }

        private void WaitComplitePrice()
        {
            _driver.WaitElement(FilterPrice);
        }

        public void ProcessingComplite()
        {
            _driver.WaitWhileElementClassContainsText(SchemaFilter, "schema-filter-button__state_animated");
        }

        public void WaitProcessing()
        {
            _driver.WaitElement(SchemaFilterProcessing);
        }
    }
}
