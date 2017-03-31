using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onliner_tests.PageObject.OrderPageObj
{
    class BasicOrderPage
    {
        private LoggerClass _log;
        private WebDriver _driver;

        public BasicOrderPage(WebDriver driver, LoggerClass log)
        {
            _driver = driver;
            _log = log;
        }

        public By ShowOrderLink { get; set; } = By.CssSelector(".schema-order__link");
        public By OrderPopular { get; set; } = By.CssSelector(".schema-order__item:nth-child(1)");
        public By OrderPriceASC { get; set; } = By.CssSelector(".schema-order__item:nth-child(2)");
        public By OrderPriceDESC { get; set; } = By.CssSelector(".schema-order__item:nth-child(3)");
        public By OrderNew { get; set; } = By.CssSelector(".schema-order__item:nth-child(4)");
        public By OrderRating { get; set; } = By.CssSelector(".schema-order__item:nth-child(5)");
        public By OrderOpen { get; set; } = By.CssSelector(".schema-order_opened");

        
        public enum OrderType
        {
            Popular, PriceASC, PriceDESC, New, Rating
        }

        public void ClickOrder(OrderType orderType)
        {
            if (GetOrdertypeCheckout() != orderType)
            {
                _driver.Click(ShowOrderLink);
                _driver.WaitElement(OrderOpen);
                switch (orderType)
                {
                    case OrderType.PriceASC:
                        _driver.WaitElement(OrderPriceASC);
                        _driver.Click(OrderPriceASC);
                        break;
                    case OrderType.PriceDESC:
                        _driver.WaitElement(OrderPriceDESC);
                        _driver.Click(OrderPriceDESC);
                        break;
                    case OrderType.New:
                        _driver.WaitElement(OrderNew);
                        _driver.Click(OrderNew);
                        break;
                    case OrderType.Popular:
                        _driver.WaitElement(OrderPopular);
                        _driver.Click(OrderPopular);
                        break;
                    case OrderType.Rating:
                        _driver.WaitElement(OrderRating);
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
            //selectOrder.Add(OrderPopular, OrderType.Popular);
            //selectOrder.Add(OrderPriceASC, OrderType.PriceASC);
            //selectOrder.Add(OrderPriceDESC, OrderType.PriceDESC);
            //selectOrder.Add(OrderNew, OrderType.New);
            //selectOrder.Add(OrderRating, OrderType.Rating);

            foreach (var element in selectOrder)
            {
                if (_driver.CheckClassForElement(element.Key, "schema-order__item_active"))
                    return element.Value;
            }

            return OrderType.Popular;
        }

    }
}
