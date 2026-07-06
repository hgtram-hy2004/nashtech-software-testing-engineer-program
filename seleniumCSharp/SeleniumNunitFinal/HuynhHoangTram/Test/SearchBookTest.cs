using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using HuynhHoangTram.Page;
using HuynhHoangTram.Utils;
using HuynhHoangTram.DataObject;
using OpenQA.Selenium;

namespace HuynhHoangTram.Test
{
    public class SearchBookTest : BaseTest
    {
        private BookStorePage bookStorePage = null!;
        HomePage home = new HomePage();
        [SetUp]
        public void PageSetUp()
        {
            bookStorePage = new BookStorePage();
            home.NavigateToBook();
            bookStorePage.NavigateToBookStore();
            //DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl") + "books");
            Assert.That(
                bookStorePage.IsBookStorePageDisplayed(),
                Is.True,
                "Book Store page is not displayed."
            );
        }
        [Test]
        [TestCase("Design")]
        [TestCase("design")]
        public void SearchBookWithMultipleResultsSuccessfully(string bookName)
        {
            bookStorePage.SearchBook(bookName);

            List<IWebElement> bookRows = bookStorePage.GetDisplayedBookRows();

            Assert.Multiple(() =>
            {
                Assert.That(
                    bookRows.Count,
                    Is.GreaterThan(0),
                    "No book is displayed after searching."
                );

                foreach (IWebElement row in bookRows)
                {
                    string title = bookStorePage.GetBookCellText(row, "Title");
                    string author = bookStorePage.GetBookCellText(row, "Author");
                    string publisher = bookStorePage.GetBookCellText(row, "Publisher");

                    Assert.That(
                        title,
                        Does.Contain(bookName).IgnoreCase,
                        $"Book title '{title}' does not match search criteria '{bookName}'."
                    );

                    Assert.That(
                        author,
                        Is.Not.Empty,
                        $"Author is empty for book '{title}'."
                    );

                    Assert.That(
                        publisher,
                        Is.Not.Empty,
                        $"Publisher is empty for book '{title}'."
                    );

                    Assert.That(
                        bookStorePage.IsBookImageDisplayed(row),
                        Is.True,
                        $"Image is not displayed for book '{title}'."
                    );
                }
            });
        }
    }
}