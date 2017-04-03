using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Onliner_tests
{
    public class WebDriver
    {
        public IWebDriver Driver { get; }
        private IWait<IWebDriver> _wait;
        private LoggerClass _log;
        private const int _waitTimeout = 20;

        //public IWebDriver Driver { get; } 

        public WebDriver(LoggerClass log)
        {
            Driver = new ChromeDriver();
            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(_waitTimeout));
            _log = log;
        }

        public WebDriver(string driverType, LoggerClass log)
        {
            if (ConfigurationManager.AppSettings.Get("Grid") == "true")
            {
                DesiredCapabilities capabilities = new DesiredCapabilities();
                switch (driverType)
                {
                    case "Firefox":
                        capabilities = DesiredCapabilities.Firefox();
                        capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
                        break;
                    case "IE":
                        capabilities = DesiredCapabilities.InternetExplorer();
                        capabilities.SetCapability(CapabilityType.BrowserName, "internet explorer");
                        break;
                    case "Chrome":
                        capabilities = DesiredCapabilities.Chrome();
                        capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
                        break;
                    default:
                        capabilities = DesiredCapabilities.Chrome();
                        capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
                        break;
                }
                switch (ConfigurationManager.AppSettings.Get("PlatformType"))
                {
                    case "Windows":
                        capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                        break;
                    case "Linux":
                        capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Linux));
                        break;
                    case "Mac":
                        capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Mac));
                        break;
                    case "Unix":
                        capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Unix));
                        break;
                    case "XP":
                        capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.XP));
                        break;
                    default:
                        capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                        break;
                }

                capabilities.SetCapability("marionette", true);
                Driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("localhost") + ":" + ConfigurationManager.AppSettings.Get("port") + "/wd/hub"), capabilities);
            }
            else
            {
                switch (driverType)
                {
                    case "Firefox":
                        Driver = new FirefoxDriver();
                        break;
                    case "IE":
                        Driver = new InternetExplorerDriver();
                        break;
                    case "Chrome":
                        Driver = new ChromeDriver();
                        break;
                    default:
                        Driver = new ChromeDriver();
                        break;
                }
            }

            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(_waitTimeout));
            _log = log;
        }

        public void Quit()
        {
            _log.Log("Test Quit");
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Quit();
        }

        public void Navigate(string url)
        {
            _log.Log($"Navigating is {url}");
            Driver.Navigate().GoToUrl(url);
            //_driver.Manage().Window.Maximize();
        }

        public void Click(IWebElement element)
        {
            _log.Log($"Click to WebElement {element.TagName}");
            WaitForElementIsVisible(element);
            element.Click();
        }

        public void SendKeys(By locator, string text)
        {
            _log.Log($"Text '{text}' entered in the locator {locator}");
            var element = FindElementWithWaiting(locator);
            element.SendKeys(text);
        }

        public void Click(By locator)
        {
            _log.Log($"Click to locator {locator}");
            var el = FindElementWithWaiting(locator);
            el.Click();
        }

        public string GetTitle()
        {
            _log.Log($"Get Title page ({Driver.Title})");
            return Driver.Title;
        }

        public string GetText(IWebElement element)
        {
            _log.Log($"Get WebElement text ({element.Text})");
            return element.Text;
        }

        public string GetText(By locator)
        {
            string text = FindElementWithWaiting(locator).Text;
            _log.Log($"Get text locator {locator} ({text})");
            return text;
        }

        public IWebElement WaitElement(By locator)
        {
            _log.Log($"Waiting locator {locator} ");
            IWebElement element = _wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element;
        }

        public void WaitElementAll(By locator)
        {
            _log.Log($"Waiting all elements by locator {locator} ");
            _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }

        public IList<IWebElement> FindAllElements(By locator)
        {
            _log.Log($"Getting the list of elements by locator {locator} ");
            IList<IWebElement> allElements = Driver.FindElements(locator);
            return allElements;
        }

        public IWebDriver GetNativeDriver() => Driver;

        public void WaitForElementIsVisible(IWebElement element, int timeout = _waitTimeout)
        {
            _log.Log($"Wait for Element is visible {element.TagName} ");
            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => element.Displayed);
        }

        public IWebElement FindElementWithWaiting(By by, int timeout = _waitTimeout)
        {
            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(d => d.FindElement(by));
        }

        public void WaitWhileElementClassContainsText(By by, string text, int timeout = _waitTimeout)
        {
            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => !d.FindElement(by).GetAttribute("class").Contains(text));
        }

        public bool CheckClassForElement(By locator, string classCheck)
        {
            bool flag = false;
            var element = FindElementWithWaiting(locator);
            _log.Log($"Check class \"{classCheck}\" for locator {locator}");
            if (element.GetAttribute("class").Contains(classCheck))
            {
                _log.Log($"Class \"{classCheck}\" is present for locator {locator}");
                flag = true;
            }
            return flag;
        }

        public void Scroll(string pix)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Driver;
            jse.ExecuteScript($"scroll(0, {pix});");
        }

        public IWebElement GetElement(By locator)
        {
            var el = FindElementWithWaiting(locator);
            return el;
        }

    }
}
