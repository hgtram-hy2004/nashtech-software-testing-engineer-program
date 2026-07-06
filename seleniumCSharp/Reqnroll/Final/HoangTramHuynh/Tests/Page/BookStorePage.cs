using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.Component;
using OpenQA.Selenium;

namespace HoangTramHuynh.Page
{
    public class BookStorePage
    {
        private readonly Menu _sideBarMenu;
        public BookStorePage()
        {
            _sideBarMenu = new Menu(SideBarMenu, ItemSideBarMenu);
        }
        private WebObject SideBarMenu(string menuText)
        {
            return new WebObject(By.XPath( $"//div[contains(@class,'left-pannel')]//div[contains(@class,'header-text') and normalize-space()='{menuText}']"), menuText + " Menu");
        }

        private WebObject ItemSideBarMenu(string itemText)
        {
            return new WebObject(By.XPath($"//div[contains(@class,'left-pannel')]//a[contains(@class,'router-link')][.//span[normalize-space()='{itemText}']]"), itemText + " Item Menu");
        }
        private readonly WebObject _searchBox = new WebObject(By.Id("searchBox"),"Search Box");
        private readonly Component.Table _bookTable =new Component.Table(By.XPath("//table"),"Book Table");
        public void NavigateToBookStore()
        {
            _sideBarMenu.SelectMenuItem("Book Store Application");
            ReportLog.Info("Navigated to Book Store page.");
        }
        public void NavigateToLogin()
        {
            _sideBarMenu.SelectMenuItem("Login");
            ReportLog.Info("Navigated to Login page.");
        }

        public bool IsBookStorePageDisplayed()
        {
            ReportLog.Info("Checking if Book Store page is displayed.");
            return _searchBox.IsElementDisplayed()
                && _bookTable.IsElementDisplayed();
        }

        public void SearchBook(string bookName)
        {
            _searchBox.EnterText(bookName);
            ReportLog.Info($"Searched for book with name: {bookName}");
        }

        public bool AreAllBooksMatchedSearchCriteria(string bookName)
        {
            ReportLog.Info($"Checking if all displayed books match the search criteria: {bookName}");
            return _bookTable.AreAllColumnValuesContains("Title",bookName);
        }
        public List<IWebElement> GetDisplayedBookRows()
        {
            ReportLog.Info("Retrieving displayed book rows.");
            return _bookTable.GetRows();
        }
        public bool HasSearchResult()
        {
            ReportLog.Info("Checking if there are any search results displayed.");
            return GetDisplayedBookRows().Count > 0;
        }
    }
}