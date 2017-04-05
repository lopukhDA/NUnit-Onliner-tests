using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Onliner_tests.PageObject.OrderPageObj
{
    class BasicOrderComponent
    {
        private WebDriver _driver;

        public BasicOrderComponent(WebDriver driver)
        {
            _driver = driver;
        }

        public By ShowOrderLink { get; set; } = By.CssSelector(".schema-order__link");
        public By OrderOpen { get; set; } = By.CssSelector(".schema-order_opened");

        private string _orderProductFormat = ".schema-order__item:nth-child({0})";
        private string _productTypeFormat = "input[value={0}] + span";

        public enum OrderType
        {
            Popular = 1,
            PriceASC,
            PriceDESC,
            New,
            Rating
        }

        private By CreateOrderLocator(OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.Popular:
                    return By.CssSelector(String.Format(_orderProductFormat, (int)OrderType.Popular));
                case OrderType.PriceASC:
                    return By.CssSelector(String.Format(_orderProductFormat, (int)OrderType.PriceASC));
                case OrderType.PriceDESC:
                    return By.CssSelector(String.Format(_orderProductFormat, (int)OrderType.PriceDESC));
                case OrderType.New:
                    return By.CssSelector(String.Format(_orderProductFormat, (int)OrderType.New));
                case OrderType.Rating:
                    return By.CssSelector(String.Format(_orderProductFormat, (int)OrderType.Rating));
                default:
                    throw new Exception($"Order type {orderType.ToString()} not defined");
            }
        }

        public enum ProductType
        {
            All = 1,
            New,
            Used
        }

        private By CreateProductTypeLocator(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.All:
                    return By.CssSelector(String.Format(_productTypeFormat, "all"));
                case ProductType.New:
                    return By.CssSelector(String.Format(_productTypeFormat, "new"));
                case ProductType.Used:
                    return By.CssSelector(String.Format(_productTypeFormat, "second"));
                default:
                    throw new Exception($"Product type {productType.ToString()} not defined");
            }
        }

        public void ClickProductType(ProductType productType)
        {
            if (GetProductTypeCheckout() != productType)
            {
                _driver.Click(CreateProductTypeLocator(productType));
            }
        }

        public void ClickOrder(OrderType orderType)
        {

            if (GetOrdertypeCheckout() != orderType)
            {
                _driver.WaitForElementIsVisible(ShowOrderLink);
                _driver.WaitForElementIsVisibleAndClick(ShowOrderLink);
                _driver.WaitForElementIsVisible(OrderOpen);
                _driver.WaitForElementIsVisibleAndClick(CreateOrderLocator(orderType));
            }
        }

        private OrderType GetOrdertypeCheckout()
        {
            Dictionary<By, OrderType> selectOrder = new Dictionary<By, OrderType>()
            {
                {CreateOrderLocator(OrderType.Popular), OrderType.Popular},
                {CreateOrderLocator(OrderType.PriceASC), OrderType.PriceASC},
                {CreateOrderLocator(OrderType.PriceDESC), OrderType.PriceDESC},
                {CreateOrderLocator(OrderType.New), OrderType.New},
                {CreateOrderLocator(OrderType.Rating), OrderType.Rating},
            };

            foreach (var element in selectOrder)
            {
                if (_driver.CheckClassForElement(element.Key, "schema-order__item_active"))
                    return element.Value;
            }

            return OrderType.Popular;
        }

        private ProductType GetProductTypeCheckout()
        {
            Dictionary<By, ProductType> selectProductType = new Dictionary<By, ProductType>()
            {
                {CreateProductTypeLocator(ProductType.All), ProductType.All},
                {CreateProductTypeLocator(ProductType.New), ProductType.New},
                {CreateProductTypeLocator(ProductType.Used), ProductType.Used}
            };

            foreach (var element in selectProductType)
            {
                if (_driver.GetElement(element.Key).GetCssValue("background").Equals("#555"))
                    return element.Value;
            }

            return ProductType.All;
        }
    }
}
