using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Core;
using HuynhHoangTram.Component;
using HuynhHoangTram.Utils;
using OpenQA.Selenium;

namespace HuynhHoangTram.Page
{
    public class ProfilePage
    {
        private readonly Menu _sideBarMenu;

        public ProfilePage()
        {
            _sideBarMenu = new Menu(
                SideBarParentMenu,
                SideBarChildMenu
            );
        }

        private WebObject SideBarParentMenu(string menuText)
        {
            return new WebObject(
                By.XPath($"//div[contains(@class,'left-pannel')]//div[contains(@class,'header-text') and normalize-space(.)='{menuText}']/ancestor::div[contains(@class,'header-wrapper')]"),
                menuText + " Parent Menu"
            );
        }

        private WebObject SideBarChildMenu(string menuText)
        {
            return new WebObject(
                By.XPath($"//div[contains(@class,'left-pannel')]//span[@class='text' and normalize-space(.)='{menuText}']/ancestor::a"),
                menuText + " Child Menu"
            );
        }
        private readonly WebObject _bookstoreButton =
            new WebObject(By.Id("gotoStore"), "Go To Book Store Button");
        private readonly WebObject _searchBox =
            new WebObject(By.Id("searchBox"), "Search Box");

        private readonly Table _bookTable =
            new Table(By.XPath("//table"), "Profile Book Table");

        private readonly WebObject _deleteBookModal =
            new WebObject(
                By.XPath("//div[contains(@class,'modal-content')]"),
                "Delete Book Modal");

        private readonly WebObject _deleteBookModalTitle =
            new WebObject(
                By.XPath("//div[@id='example-modal-sizes-title-sm' and normalize-space(.)='Delete Book']"),
                "Delete Book Modal Title"
            );

        private readonly WebObject _confirmOkButton =
            new WebObject(
                By.Id("closeSmallModal-ok"),
                "Confirm OK Button"
            );

        private WebObject DeleteBookIcon(string bookName)
        {
            return new WebObject(
                By.XPath($"//a[normalize-space(.)='{bookName}']/ancestor::tr//span[contains(@id,'delete-record')]"),
                "Delete Icon Of " + bookName
            );
        }

        public void NavigateToProfile()
        {
            _sideBarMenu.SelectMenuItem("Profile");
        }

        public bool IsProfilePageDisplayed()
        {
            string expectedUrl = ConfigurationUtils.GetConfigurationByKey("TestUrl") + "profile";

            return expectedUrl.Contains(
                    "/profile",
                    StringComparison.OrdinalIgnoreCase
                )
                && _searchBox.IsElementDisplayed();
        }
        public bool IsBookDisplayed(string bookName)
        {
            return _bookTable.IsColumnValueDisplayed(
                "Title",
                bookName
            );
        }
        public void SearchBook(string bookName)
        {
            _searchBox.EnterText(bookName);
        }
        public bool IsBookExistingInProfile(string bookName)
        {
            SearchBook(bookName);

            return _bookTable.IsColumnValueDisplayed(
                "Title",
                bookName,
                3
            );
        }
        public void ClickBookStoreButton()
        {
            _bookstoreButton.ClickOnElement();
        }
        public void ClickDeleteBookIcon(string bookName)
        {
            DeleteBookIcon(bookName).ClickOnElement();
        }
        public bool IsDeleteBookModalDisplayed()
        {
            return _deleteBookModal.IsElementDisplayed()
                && _deleteBookModalTitle.IsElementDisplayed();
        }

        public void ClickConfirmOkButton()
        {
            _confirmOkButton.ClickOnElement();
        }
        public bool IsBookDisplayedWithoutSearch(string bookName)
        {
            try
            {
                return _bookTable.GetColumnTexts("Title")
                    .Any(title => title.Equals(
                        bookName,
                        StringComparison.OrdinalIgnoreCase
                    ));
            }
            catch
            {
                return false;
            }
        }
        public string DeleteBook(string bookName)
        {
            SearchBook(bookName);

            if (!IsBookDisplayedWithoutSearch(bookName))
            {
                throw new Exception($"Book '{bookName}' is not displayed in Profile.");
            }

            ClickDeleteBookIcon(bookName);

            if (!IsDeleteBookModalDisplayed())
            {
                throw new Exception("Delete Book confirmation modal is not displayed.");
            }

            ClickConfirmOkButton();

            return AlertUtils.AcceptAlertAndGetText();
        }
    }
}