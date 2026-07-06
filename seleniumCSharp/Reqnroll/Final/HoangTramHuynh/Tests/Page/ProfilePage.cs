using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.Component;
using HoangTramHuynh.Utils;
using OpenQA.Selenium;

namespace HoangTramHuynh.Page
{
    public class ProfilePage
    {
        private readonly Menu _sideBarMenu;

        public ProfilePage()
        {
            _sideBarMenu = new Menu(SideBarParentMenu,SideBarChildMenu);
        }

        private WebObject SideBarParentMenu(string menuText)
        {
            return new WebObject(
                By.XPath($"//div[contains(@class,'left-pannel')]//div[contains(@class,'header-text') and normalize-space()='{menuText}']"),$"{menuText} Parent Menu");
        }

        private WebObject SideBarChildMenu(string itemText)
        {
            return new WebObject(
                By.XPath($"//div[contains(@class,'left-pannel')]//a[contains(@class,'router-link')][.//span[normalize-space()='{itemText}']]"),$"{itemText} Child Menu");
        }
        private readonly WebObject _profileTitle  = new WebObject(By.Id("userName-label"), "Profile Title");
        private readonly WebObject _bookstoreButton = new WebObject(By.Id("gotoStore"), "Go To Book Store Button");
        private readonly WebObject _searchBox = new WebObject(By.Id("searchBox"), "Search Box");
        private readonly Component.Table _bookTable = new Component.Table(By.XPath("//table"), "Profile Book Table");
        private readonly WebObject _deleteAccountButton = new WebObject(By.XPath("//button[normalize-space()='Delete Account']"), "Delete Account Button");
        private readonly WebObject _deleteBookModal =new WebObject(By.XPath("//div[contains(@class,'modal-content')]"),"Delete Book Modal");
        private readonly WebObject _deleteBookModalTitle =new WebObject(By.XPath("//div[@id='example-modal-sizes-title-sm' and normalize-space(.)='Delete Book']"),"Delete Book Modal Title");
        private readonly WebObject _confirmOkButton =new WebObject(By.Id("closeSmallModal-ok"),"Confirm OK Button");
        private WebObject DeleteBookIcon(string bookName)
        {
            return new WebObject(By.XPath($"//a[normalize-space(.)='{bookName}']/ancestor::tr//span[contains(@id,'delete-record')]"),"Delete Icon Of " + bookName);
        }
        public bool IsProfilePageDisplayed()
        {
            ReportLog.Info("Checking if Profile Page is displayed.");
            return _profileTitle.IsElementDisplayed()
                    && _bookstoreButton.IsElementDisplayed()
                    && _deleteAccountButton.IsElementDisplayed();
        }
        public void SearchBook(string bookName)
        {
            _searchBox.EnterText(bookName);
            ReportLog.Info($"Searched for book: {bookName}");
        }
        public bool IsBookExistingInProfile(string bookName)
        {
            SearchBook(bookName);
            ReportLog.Info($"Checking if book '{bookName}' exists in the profile.");
            return _bookTable.IsColumnValueDisplayed("Title",bookName,3);

        }
        public void ClickDeleteBookIcon(string bookName)
        {
            DeleteBookIcon(bookName).ClickOnElement();
            ReportLog.Info($"Clicked on Delete icon of book: {bookName}");
        }
        public bool IsDeleteBookModalDisplayed()
        {
            ReportLog.Info("Checking if Delete Book confirmation modal is displayed.");
            return _deleteBookModal.IsElementDisplayed()
                && _deleteBookModalTitle.IsElementDisplayed();
        }

        public void ClickConfirmOkButton()
        {
            ReportLog.Info("Clicking on Confirm OK button in Delete Book modal.");
            _confirmOkButton.ClickOnElement();
        }
    }
}