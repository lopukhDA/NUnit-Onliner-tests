using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onliner_tests
{
    public class WebDriver
    {
        private IWebDriver _driver;
        private IWait<IWebDriver> _wait;

        public WebDriver()  
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public WebDriver(string driverType)
        {
            switch (driverType)
            {
                case "FirefoxDriver":
                    _driver = new FirefoxDriver();
                    break;
                case "InternetExplorerDriver":
                    _driver = new InternetExplorerDriver();
                    break;
                case "ChromeDriver":
                    _driver = new ChromeDriver();
                    break;
                default:
                    _driver = new ChromeDriver();
                    break;
            }
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public void Quit()
        {
            Console.WriteLine("Test Quit");
            _driver.Quit();
        }

        public void Navigate(string url)
        {
            Console.WriteLine($"Navigating is {url}");
            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();
        }

        public void Click(IWebElement element)
        {
            WaitForElementIsVisible(element);
            element.Click();
        }

        public void SendKeys(By locator, string text)
        {
            Console.WriteLine($"Text '{text}' entered in the locator {locator}");
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
