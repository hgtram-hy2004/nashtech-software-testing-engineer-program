using System.Net;
using HoangTramHuynh.Contexts;
using HoangTramHuynh.Core.API;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.DataObject;
using HoangTramHuynh.Models.Request;
using HoangTramHuynh.Models.Response;
using HoangTramHuynh.Page;
using HoangTramHuynh.Services;
using HoangTramHuynh.Utils;
using NUnit.Framework;
using Reqnroll;
using RestSharp;

namespace HoangTramHuynh.StepDefinitions
{
    [Binding]
    public class DeleteBookStep
    {
        private readonly LoginPage _loginPage = new LoginPage();
        private readonly BookStorePage _bookStorePage = new BookStorePage();
        private readonly ProfilePage _profilePage = new ProfilePage();
		private readonly HomePage _homePage = new HomePage();

        private readonly DeleteBookContext _deleteBookContext;
        private readonly AccountService _accountService;
        private readonly BookStoreService _bookStoreService;

        public DeleteBookStep(DeleteBookContext deleteBookContext)
        {
            _deleteBookContext = deleteBookContext;

            string apiUrl = ConfigurationUtils.GetConfigurationByKey("TestUrl");
            APIClient apiClient = new APIClient(apiUrl);

            _accountService = new AccountService(apiClient);
            _bookStoreService = new BookStoreService(apiClient);
        }

        [Given("the book is available in the user's collection")]
		public async Task GivenTheBookIsAvailableInTheUsersCollectionByAPI(Table table)
		{
			BookData deleteBookData = table.CreateInstance<BookData>();

			_deleteBookContext.BookData.BookName = deleteBookData.BookName;
			_deleteBookContext.BookData.Username = deleteBookData.Username;
			_deleteBookContext.BookData.Password = deleteBookData.Password;

			ReportLog.Info($"Book Name: {_deleteBookContext.BookData.BookName}");
			ReportLog.Info($"Username: {_deleteBookContext.BookData.Username}");

			AccountResponse accountResponse = await _accountService.LoginAsync(_deleteBookContext.BookData.Username,_deleteBookContext.BookData.Password);

			_deleteBookContext.UserId = accountResponse.UserId;
			_deleteBookContext.Token = accountResponse.Token;

			Assert.That(_deleteBookContext.UserId,Is.Not.Empty,"UserId is empty after login by API.");
			Assert.That(_deleteBookContext.Token,Is.Not.Empty,"Token is empty after login by API.");

			_deleteBookContext.BookData.Isbn = await _bookStoreService.GetIsbnByBookNameAsync(_deleteBookContext.BookData.BookName);
			Assert.That(_deleteBookContext.BookData.Isbn,Is.Not.Empty,$"ISBN is empty for book '{_deleteBookContext.BookData.BookName}'.");
			bool isBookExisting = await _accountService.IsBookExistingInCollectionAsync(_deleteBookContext.UserId,_deleteBookContext.Token,_deleteBookContext.BookData.Isbn);
			if (isBookExisting)
			{
				ReportLog.Info($"Add Book API: Skip adding because book '{_deleteBookContext.BookData.BookName}' already exists in collection.");
				return;
			}

			var addBookRequest = new BookStoreRequest
			{
				UserId = _deleteBookContext.UserId,
				CollectionOfIsbns = new List<IsbnDto>
				{
					new IsbnDto
					{
						Isbn = _deleteBookContext.BookData.Isbn
					}
				}
			};

			RestResponse response = await _bookStoreService.AddBookToCollectionAsync(addBookRequest,_deleteBookContext.Token);

			Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.Created),$"Add book failed. Status: {response.StatusCode}, Content: {response.Content}");

			ReportLog.Info("Pre-condition API: Book is available in user's collection.");
		}

        [Given("the user logs into the application")]
        public void GivenTheUserLogsIntoTheApplication()
        {
            _homePage.NavigateToBook();
			_bookStorePage.NavigateToLogin();
            _loginPage.Login(_deleteBookContext.BookData.Username,_deleteBookContext.BookData.Password);
        }

        [Given("the user is on the Profile page")]
        public void GivenTheUserIsOnTheProfilePage()
        {
            Assert.That(_profilePage.IsProfilePageDisplayed(),Is.True,"Profile page is not displayed after login successfully.");
            ReportLog.Info("Profile page is displayed.");
        }

        [When("the user searches for the book")]
        public void WhenTheUserSearchesForTheBook()
        {
            string bookName = _deleteBookContext.BookData.BookName;
            _profilePage.SearchBook(bookName);
            Assert.That( _profilePage.IsBookExistingInProfile(bookName), Is.True, $"Book '{bookName}' is not displayed before deleting.");
        }

        [When("the user clicks on Delete icon of the book")]
        public void WhenTheUserClicksOnDeleteIconOfTheBook()
        {
            string bookName = _deleteBookContext.BookData.BookName;
            ReportLog.Info($"Delete Book UI: Click delete icon of book '{bookName}'.");
            _profilePage.ClickDeleteBookIcon(bookName);
        }

        [When("the user clicks on OK button")]
        public void WhenTheUserClicksOnOKButton()
        {
            ReportLog.Info("Delete Book UI: Check delete confirmation modal is displayed.");
            Assert.That(_profilePage.IsDeleteBookModalDisplayed(),Is.True,"Delete Book confirmation modal is not displayed.");
            ReportLog.Info("Delete Book UI: Click OK button on confirmation modal.");
            _profilePage.ClickConfirmOkButton();
        }

        [When("the user clicks on OK button of alert {string}")]
        public void WhenTheUserClicksOnOKButtonOfAlert(string expectedAlertMessage)
        {
            ReportLog.Info($"Delete Book UI: Verify alert message '{expectedAlertMessage}'.");
            string actualAlertMessage = AlertUtils.AcceptAlertAndGetText();
            Assert.That(actualAlertMessage,Is.EqualTo(expectedAlertMessage),"Alert message is not correct.");

            ReportLog.Info($"Delete Book UI: Alert message is correct: '{actualAlertMessage}'.");
        }

        [Then("the book is not shown")]
        public void ThenTheBookIsNotShown()
        {
            string bookName = _deleteBookContext.BookData.BookName;
            ReportLog.Info($"Verify UI: Check book '{bookName}' is not shown after deleting.");
            Assert.That( _profilePage.IsBookExistingInProfile(bookName),Is.False,$"Book '{bookName}' is still displayed after deleting.");
            ReportLog.Info($"Verify UI: Book '{bookName}' is deleted successfully.");
        }
    }
}