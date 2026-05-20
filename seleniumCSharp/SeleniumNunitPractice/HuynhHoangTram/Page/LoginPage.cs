using System;
using System.Threading;
using System.Linq;
using HuynhHoangTram.Core;
using OpenQA.Selenium;
namespace HuynhHoangTram.Page;

public class LoginPage
{
    private WebObject _userNameInput = new WebObject(By.CssSelector("input#username"), "Username Input");

    private WebObject _passwordInput = new WebObject(By.CssSelector("input#password"), "Password Input");

    private WebObject _loginBtn = new WebObject(By.XPath("//input[@value='Login']"), "Login button");

    private WebObject _errorMessage = new WebObject(By.CssSelector("form[name='loginForm'] div.alert-danger"),"Error Message");
    private WebObject _usernamerequiredmessage = new WebObject(By.XPath("//div[@ng-messages='submitted && loginForm.username.$error']"), "Username Required Message");

    private WebObject _passwordrequiredmessage = new WebObject(By.XPath("//div[@ng-messages='submitted && loginForm.password.$error']"), "Password Required Message");

    public LoginPage()
    {
    }

    public void EnterUsername(string username)
    {
        _userNameInput.EnterText(username);
    }

    public void EnterPassword(string password)
    {
        _passwordInput.EnterText(password);
    }

    public void ClickLogin()
    {
        _loginBtn.ClickOnElement();
    }

     public bool IsLoginFailed()
    {
        return WebObjectExtension.WaitUntilAnyElementDisplayed(
            _errorMessage,
            _usernamerequiredmessage,
            _passwordrequiredmessage
        );
    }
    public void Login(string username, string password)
    {
        EnterUsername(username);

        EnterPassword(password);

        ClickLogin();
    }
}
