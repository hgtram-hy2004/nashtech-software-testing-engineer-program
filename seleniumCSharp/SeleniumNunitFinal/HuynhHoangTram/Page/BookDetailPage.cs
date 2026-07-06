using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Core;
using HuynhHoangTram.Utils;
using OpenQA.Selenium;

namespace HuynhHoangTram.Page
{
    public class BookDetailPage
    {
        private readonly WebObject _bookStoreTitle =
            new WebObject(
                By.XPath("//h1[normalize-space(.)='Book Store']"),
                "Book Store Title"
            );

        private readonly WebObject _isbnLabel =
            new WebObject(
                By.XPath("//div[@id='ISBN-wrapper']//label[normalize-space(.)='ISBN:']"),
                "ISBN Label"
            );

        private readonly WebObject _bookTitleLabel =
            new WebObject(
                By.Id("title-label"),
                "Book Title Label"
            );

        private readonly WebObject _addToCollectionButton =
            new WebObject(
                By.XPath("//button[normalize-space(.)='Add To Your Collection']"),
                "Add To Your Collection Button"
            );

        private WebObject BookTitleValue(string bookName)
        {
            return new WebObject(
                By.XPath($"//div[@id='title-wrapper']//label[normalize-space(.)='{bookName}']"),
                bookName + " Book Title Value"
            );
        }
        public bool IsBookDetailDisplayed(string bookName)
        {
            return _bookStoreTitle.IsElementDisplayed()
                && _isbnLabel.IsElementDisplayed()
                && _bookTitleLabel.IsElementDisplayed()
                && BookTitleValue(bookName).IsElementDisplayed();
        }

        public void ClickAddToCollectionButton()
        {
            _addToCollectionButton.ScrollToElement();
            _addToCollectionButton.ClickOnElement();
        }
        public string AddBookToCollection()
        {
            ClickAddToCollectionButton();

            return AlertUtils.AcceptAlertAndGetText();
        }
    }
}