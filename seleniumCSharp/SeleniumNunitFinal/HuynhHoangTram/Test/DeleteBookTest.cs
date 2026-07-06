using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using HuynhHoangTram.Page;
using HuynhHoangTram.Utils;
using HuynhHoangTram.Report;
using HuynhHoangTram.Flow;
using HuynhHoangTram.DataObject;
using OpenQA.Selenium;

namespace HuynhHoangTram.Test
{
    public class DeleteBookTest : BaseTest
    {
        private LoginPage loginPage = null!;
        private BookStorePage bookStorePage = null!;
        private BookDetailPage bookDetailPage = null!;
        private ProfilePage profilePage = null!;
        private DeleteBookFlow deleteBookFlow = null!;
        [SetUp]
        public void PageSetUp()
        {
            loginPage = new LoginPage();
            bookStorePage = new BookStorePage();
            bookDetailPage = new BookDetailPage();
            profilePage = new ProfilePage();
            deleteBookFlow = new DeleteBookFlow(
                loginPage,
                profilePage,
                bookStorePage,
                bookDetailPage
            );
            DriverUtils.GoToUrl(
                ConfigurationUtils.GetConfigurationByKey("TestUrl") + "login"
            );
            Assert.That(
                loginPage.IsLoginPageDisplayed(),
                Is.True,
                "Login page is not displayed."
            );
        }
        [TestCase("hgtram.hy04", "Hgtram@hy2004", "Git Pocket Guide")]
        public void DeleteBookSuccessfully(string username, string password, string bookName)
        {
            deleteBookFlow.DeleteBookSuccessfully(
                username,
                password,
                bookName
            );
        }
    }
}