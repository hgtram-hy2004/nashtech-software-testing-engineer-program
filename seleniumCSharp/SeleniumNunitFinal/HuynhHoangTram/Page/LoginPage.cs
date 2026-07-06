using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HuynhHoangTram.Core;

namespace HuynhHoangTram.Page
{
    public class LoginPage
    {
        private readonly WebObject _loginTitle =
            new WebObject(
                By.XPath("//h1[normalize-space(.)='Login']"),
                "Login Title"
            );

        private readonly WebObject _usernameInput =
            new WebObject(
                By.Id("userName"),
                "Username Input"
            );

        private readonly WebObject _passwordInput =
            new WebObject(
                By.Id("password"),
                "Password Input"
            );

        private readonly WebObject _loginButton =
            new WebObject(
                By.Id("login"),
                "Login Button"
            );

        private readonly WebObject _errorMessage =
            new WebObject(
                By.Id("name"),
                "Login Error Message"
            );

        public bool IsLoginPageDisplayed()
        {
            return _loginTitle.IsElementDisplayed()
                && _usernameInput.IsElementDisplayed()
                && _passwordInput.IsElementDisplayed()
                && _loginButton.IsElementDisplayed();
        }

        public void EnterUsername(string username)
        {
            _usernameInput.EnterText(username);
        }

        public void EnterPassword(string password)
        {
            _passwordInput.EnterText(password);
        }

        public void ClickLoginButton()
        {
            _loginButton.ClickOnElement();
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);

            EnterPassword(password);

            ClickLoginButton();
        }

        public bool IsLoginFailed()
        {
            return _errorMessage.IsElementDisplayed();
        }

        public string GetErrorMessage()
        {
            return _errorMessage.GetText();
        }
    }
}