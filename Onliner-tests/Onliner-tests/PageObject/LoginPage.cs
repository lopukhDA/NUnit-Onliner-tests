﻿using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace Onliner_tests.PageObject
{
    public class LoginPage
    {
        private WebDriver _driver;
        private LoggerClass _log;

        public LoginPage(WebDriver driver, LoggerClass log)
        {
            _driver = driver;
            _log = log;
        }
        

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
