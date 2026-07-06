using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.Utils;

namespace HoangTramHuynh.Page
{
    public class LoginPage
    {
        private readonly WebObject _loginTitle = new WebObject(By.XPath("//h1[normalize-space(.)='Login']"),"Login Title");
        private readonly WebObject _usernameInput = new WebObject(By.Id("userName"),"Username Input");
        private readonly WebObject _passwordInput =new WebObject(By.Id("password"),"Password Input");
        private readonly WebObject _loginButton =new WebObject(By.Id("login"),"Login Button");
        public bool IsLoginPageDisplayed()
        {
            ReportLog.Info("Checking if Login page is displayed.");
            return _loginTitle.IsElementDisplayed()
                && _usernameInput.IsElementDisplayed()
                && _passwordInput.IsElementDisplayed()
                && _loginButton.IsElementDisplayed();
        }
        public void EnterUsername(string username)
        {
            _usernameInput.EnterText(username);
            ReportLog.Info($"Entered username: {username}");
        }

        public void EnterPassword(string password)
        {
            _passwordInput.EnterText(password);
            ReportLog.Info($"Entered password: {new string('*', password.Length)}");
        }

        public void ClickLoginButton()
        {
            _loginButton.ClickOnElement();
            ReportLog.Info("Clicked on Login Button.");
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);

            EnterPassword(password);

            ClickLoginButton();
        }
    }
}