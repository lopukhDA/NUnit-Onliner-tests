using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Onliner_tests.PageObject
{
    public class LoginPage
    {
       
        public LoginPage(WebDriver driver)
        {
            _driver = driver;
        }
        private WebDriver _driver;

        public void Open()
        {
            _driver.Navigate("https://www.onliner.by/");
        }

        public By Username { get; set; } = By.CssSelector(".user-name a");
        public By ButtonShowFormLogin { get; set; } = By.ClassName("auth-bar__item--text");
        public By LoginInput { get; set; } = By.CssSelector("#auth-container__forms form input.auth-box__input[data-field=login][type=text]");
        public By PasswordInput { get; set; } = By.CssSelector("#auth-container__forms form input.auth-box__input[data-field=login][type=password]");
        public By SubmitLoginButton { get; set; } = By.CssSelector(".auth-box__auth-submit");

        public void Login(string username, string password)
        {
            _driver.Click(ButtonShowFormLogin);
            _driver.SendKeys(LoginInput, username);
            _driver.SendKeys(PasswordInput, password);
            _driver.Click(SubmitLoginButton);
        }
    }
}
