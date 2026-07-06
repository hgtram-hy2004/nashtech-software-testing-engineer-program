using NUnit.Framework;
using HuynhHoangTram.Page;
using HuynhHoangTram.Report;

namespace HuynhHoangTram.Flow
{
    public class DeleteBookFlow
    {
        private readonly LoginPage _loginPage;
        private readonly ProfilePage _profilePage;
        private readonly BookStorePage _bookStorePage;
        private readonly BookDetailPage _bookDetailPage;

        public DeleteBookFlow(
            LoginPage loginPage,
            ProfilePage profilePage,
            BookStorePage bookStorePage,
            BookDetailPage bookDetailPage
        )
        {
            _loginPage = loginPage;
            _profilePage = profilePage;
            _bookStorePage = bookStorePage;
            _bookDetailPage = bookDetailPage;
        }

        public void DeleteBookSuccessfully(string username, string password, string bookName)
        {
            LoginToApplication(username, password);

            bool isBookExisting = CheckBookExistingInProfile(bookName);

            if (isBookExisting)
            {
                DeleteExistingBookFlow(bookName);

                return;
            }

            AddBookThenDeleteBookFlow(bookName);
        }

        private void LoginToApplication(string username, string password)
        {
            ExtentReportHelpers.ExecuteStep("Step 1: Login to application", () =>
            {
                _loginPage.Login(username, password);

                Assert.That(
                    _profilePage.IsProfilePageDisplayed(),
                    Is.True,
                    "Profile page is not displayed after login successfully."
                );
            });
        }

        private bool CheckBookExistingInProfile(string bookName)
        {
            bool isBookExisting = false;

            ExtentReportHelpers.ExecuteStep("Step 2: Check book existing in Profile", () =>
            {
                isBookExisting = _profilePage.IsBookExistingInProfile(bookName);

                TestContext.Progress.WriteLine(
                    $"Book '{bookName}' existing in Profile: {isBookExisting}"
                );
            });

            return isBookExisting;
        }

        private void DeleteExistingBookFlow(string bookName)
        {
            ExtentReportHelpers.ExecuteStep("Step 3: Delete existing book from Profile", () =>
            {
                string deleteBookAlertText = _profilePage.DeleteBook(bookName);

                Assert.That(
                    deleteBookAlertText,
                    Is.EqualTo("Book deleted."),
                    "Delete book alert message is not correct."
                );
            });

            VerifyBookIsNotDisplayedInProfile(
                "Step 4: Verify book is not displayed after deleting",
                bookName
            );
        }

        private void AddBookThenDeleteBookFlow(string bookName)
        {
            ExtentReportHelpers.ExecuteStep("Step 3: Open Book Store page", () =>
            {
                _profilePage.ClickBookStoreButton();

                Assert.That(
                    _bookStorePage.IsBookStorePageDisplayed(),
                    Is.True,
                    "Book Store page is not displayed."
                );
            });

            ExtentReportHelpers.ExecuteStep("Step 4: Search book in Book Store Application", () =>
            {
                _bookStorePage.SearchBook(bookName);

                Assert.That(
                    _bookStorePage.AreAllBooksMatchedSearchCriteria(bookName),
                    Is.True,
                    "Book search result is not matched before adding book."
                );
            });

            ExtentReportHelpers.ExecuteStep("Step 5: Open book detail page", () =>
            {
                _bookStorePage.ClickBookTitle(bookName);

                Assert.That(
                    _bookDetailPage.IsBookDetailDisplayed(bookName),
                    Is.True,
                    "Book detail page is not displayed."
                );
            });

            ExtentReportHelpers.ExecuteStep("Step 6: Add book to collection", () =>
            {
                string addBookAlertText = _bookDetailPage.AddBookToCollection();

                Assert.That(
                    addBookAlertText,
                    Is.EqualTo("Book added to your collection."),
                    "Add book alert message is not correct."
                );
            });

            ExtentReportHelpers.ExecuteStep("Step 7: Navigate to Profile page", () =>
            {
                _profilePage.NavigateToProfile();

                Assert.That(
                    _profilePage.IsProfilePageDisplayed(),
                    Is.True,
                    "Profile page is not displayed."
                );
            });

            VerifyBookIsDisplayedInProfile(
                "Step 8: Verify book is displayed in Profile after adding",
                bookName
            );

            ExtentReportHelpers.ExecuteStep("Step 9: Delete book from Profile", () =>
            {
                string deleteBookAlertText = _profilePage.DeleteBook(bookName);

                Assert.That(
                    deleteBookAlertText,
                    Is.EqualTo("Book deleted."),
                    "Delete book alert message is not correct."
                );
            });

            VerifyBookIsNotDisplayedInProfile(
                "Step 10: Verify book is not displayed after deleting",
                bookName
            );
        }

        private void VerifyBookIsDisplayedInProfile(string stepName, string bookName)
        {
            ExtentReportHelpers.ExecuteStep(stepName, () =>
            {
                _profilePage.SearchBook(bookName);

                Assert.That(
                    _profilePage.IsBookDisplayed(bookName),
                    Is.True,
                    $"Book '{bookName}' is not displayed in Profile."
                );
            });
        }

        private void VerifyBookIsNotDisplayedInProfile(string stepName, string bookName)
        {
            ExtentReportHelpers.ExecuteStep(stepName, () =>
            {
                _profilePage.SearchBook(bookName);

                Assert.That(
                    _profilePage.IsBookDisplayed(bookName),
                    Is.False,
                    $"Book '{bookName}' is still displayed in Profile."
                );
            });
        }
    }
}