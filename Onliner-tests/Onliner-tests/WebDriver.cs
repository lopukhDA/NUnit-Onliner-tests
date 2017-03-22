using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onliner_tests
{
    public class WebDriver
    {
        private IWebDriver _driver;
        private IWait<IWebDriver> _wait;

        public WebDriver()   //public WebDriver(string driverType)
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public void Navigate(string v)
        {
            //Console.WriteLine($"Go to ulr: {v}");
            _driver.Navigate().GoToUrl(v);
        }

        public void Click(IWebElement element)
        {
            WaitForElementIsVisible(element);
            element.Click();
        }

        public void SendKeys(By locator, string text)
        {
            var element = FindElementWithWaiting(locator);
            element.SendKeys(text);
        }

        public void Click(By locator)
        {
            var el = FindElementWithWaiting(locator);
            el.Click();
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string GetText(IWebElement element)
        {
            return element.Text;
        }

        public string GetText(By locator)
        {
            return FindElementWithWaiting(locator).Text;
        }

        public IWebElement WaitElement(By locator)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element;
        }

        public IList<IWebElement> FindAllElements(By locator)
        {
            IList<IWebElement> allElements = _driver.FindElements(locator);
            return allElements;
            
        }
        
        public IWebDriver GetNativeDriver() => _driver;

        public  void WaitForElementIsVisible(IWebElement element, int timeout = 30)
        {
            IWait<IWebDriver> wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => element.Displayed);
        }

        public  IWebElement FindElementWithWaiting(By by, int timeout = 30)
        {
            IWait<IWebDriver> wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(d => d.FindElement(by));
        }

        public  void WaitWhileElementClassContainsText(By by, string text, int timeout = 30)
        {
            IWait<IWebDriver> wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => !d.FindElement(by).GetAttribute("class").Contains(text));
        }
    }

}
