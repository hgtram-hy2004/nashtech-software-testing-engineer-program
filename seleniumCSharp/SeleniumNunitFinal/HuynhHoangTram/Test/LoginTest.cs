using System;
using NUnit.Framework;
using OpenQA.Selenium;
using HuynhHoangTram.Page;
using HuynhHoangTram.DataObject;
namespace HuynhHoangTram.Test;
    [TestFixture]
    public class LoginTest : BaseTest
    {
        private LoginPage loginPage = null!;
        HomePage homePage = new HomePage(); 
        [SetUp]
        public void PageSetUp()
        {
            loginPage = new LoginPage();
        }

        [Test]
        [TestCase("Admin2", "Fxu1tw^E")]
        public void LoginWithValidAccountSuccessfully(string username,string password)
        {
            loginPage.Login(username, password);
            Assert.That(homePage.IsHomePageDisplayed(),Is.True);
            Assert.That(homePage.IsLoginWithCorrectAccount(username),Is.True);
        }

        [Test]
        [TestCaseSource(typeof(LoginTestData),nameof(LoginTestData.InvalidLoginData))]
        public void LoginWithInvalidAccount(string username,string password)
        {
            {
                loginPage.Login(username, password);

                Assert.That(loginPage.IsLoginFailed(),Is.True);
            }
        }
    }
