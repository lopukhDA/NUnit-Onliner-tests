using OpenQA.Selenium;
using System.Collections.Generic;

namespace Onliner_tests.PageObject.OrderPageObj
{
    class BasicOrderPage
    {
        private WebDriver _driver;

        public BasicOrderPage(WebDriver driver)
        {
            _driver = driver;
        }

        public By ShowOrderLink { get; set; } = By.CssSelector(".schema-order__link");
        public By OrderPopular { get; set; } = By.CssSelector(".schema-order__item:nth-child(1)");
        public By OrderPriceASC { get; set; } = By.CssSelector(".schema-order__item:nth-child(2)");
        public By OrderPriceDESC { get; set; } = By.CssSelector(".schema-order__item:nth-child(3)");
        public By OrderNew { get; set; } = By.CssSelector(".schema-order__item:nth-child(4)");
        public By OrderRating { get; set; } = By.CssSelector(".schema-order__item:nth-child(5)");
        public By OrderOpen { get; set; } = By.CssSelector(".schema-order_opened");
        public By OnlyNewProduct { get; set; } = By.CssSelector("input[name=ko_unique_2] + span");

        
        public enum OrderType
        {
            Popular, PriceASC, PriceDESC, New, Rating
        }

        public void ClickOrder(OrderType orderType)
        {
            _driver.Click(OnlyNewProduct);
            if (GetOrdertypeCheckout() != orderType)
            {
                //_driver.Click(ShowOrderLink);
                _driver.WaitForElementIsVisible(OrderOpen);
                switch (orderType)
                {
                    case OrderType.PriceASC:
                        _driver.WaitForElementIsVisible(OrderPriceASC);
                        _driver.Click(OrderPriceASC);
                        break;
                    case OrderType.PriceDESC:
                        _driver.WaitForElementIsVisible(OrderPriceDESC);
                        _driver.Click(OrderPriceDESC);
                        break;
                    case OrderType.New:
                        _driver.WaitForElementIsVisible(OrderNew);
                        _driver.Click(OrderNew);
                        break;
                    case OrderType.Popular:
                        _driver.WaitForElementIsVisible(OrderPopular);
                        _driver.Click(OrderPopular);
                        break;
                    case OrderType.Rating:
                        _driver.WaitForElementIsVisible(OrderRating);
                        _driver.Click(OrderRating);
                        break;
                }
               
            }
        }

        private OrderType GetOrdertypeCheckout()
        {
            Dictionary<By, OrderType> selectOrder = new Dictionary<By, OrderType>()
            {
                {OrderPopular, OrderType.Popular},
                {OrderPriceASC, OrderType.PriceASC},
                {OrderPriceDESC, OrderType.PriceDESC},
                {OrderNew, OrderType.New},
                {OrderRating, OrderType.Rating},
            };

            foreach (var element in selectOrder)
            {
                if (_driver.CheckClassForElement(element.Key, "schema-order__item_active"))
                    return element.Value;
            }

            return OrderType.Popular;
        }

    }
}
