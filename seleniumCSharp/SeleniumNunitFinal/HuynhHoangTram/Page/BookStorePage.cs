using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Core;
using HuynhHoangTram.Component;
using OpenQA.Selenium;

namespace HuynhHoangTram.Page
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
            return new WebObject(By.XPath($"//div[contains(@class,'left-pannel')]//div[contains(@class,'header-text') and normalize-space(.)='{menuText}']/ancestor::div[contains(@class,'header-wrapper')]"), menuText + " Menu");
        }

        private WebObject ItemSideBarMenu(string menuText)
        {
            return new WebObject(By.XPath($"//div[contains(@class,'left-pannel')]//span[@class='text' and normalize-space(.)='{menuText}']/ancestor::a"), menuText + " Item Menu");
        }
        private readonly WebObject _searchBox =
           new WebObject(
               By.Id("searchBox"),
               "Search Box"
           );

        private readonly Table _bookTable =
           new Table(
               By.XPath("//table"),
               "Book Table"
           );
        private WebObject BookTitleLink(string bookName)
        {
            return new WebObject(
                By.XPath($"//a[normalize-space(.)='{bookName}']"),
                bookName + " Book Link"
            );
        }
        public void NavigateToBookStore()
        {
            _sideBarMenu.SelectMenuItem("Book Store");
        }

        public bool IsBookStorePageDisplayed()
        {
            return _searchBox.IsElementDisplayed()
                && _bookTable.IsElementDisplayed();
        }

        public void SearchBook(string bookName)
        {
            _searchBox.EnterText(bookName);
        }

        public bool AreAllBooksMatchedSearchCriteria(string bookName)
        {
            return _bookTable.AreAllColumnValuesContains(
                "Title",
                bookName
            );
        }
        public void ClickBookTitle(string bookName)
        {
            BookTitleLink(bookName).ClickOnElement();
        }
        public List<IWebElement> GetDisplayedBookRows()
        {
            return _bookTable.GetRows();
        }

        public List<string> GetDisplayedBookTitles()
        {
            return _bookTable.GetColumnTexts("Title");
        }

        public string GetBookCellText(IWebElement row, string columnName)
        {
            return _bookTable.GetCellText(row, columnName);
        }

        public bool IsBookImageDisplayed(IWebElement row)
        {
            return _bookTable.IsImageDisplayedInCell(row, "Image");
        }
    }
}