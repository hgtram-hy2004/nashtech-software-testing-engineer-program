using System.Diagnostics;
using System.Net;
using HuynhHoangTram.Core.Assertions;
using HuynhHoangTram.Core.Report;
using HuynhHoangTram.Core.Utilities;
using HuynhHoangTram.Models;
using HuynhHoangTram.Models.Request;
using HuynhHoangTram.Models.Response;
using HuynhHoangTram.Test.Assertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HuynhHoangTram.Test
{
    [TestFixture]
    [Category("DeleteBookAPI")]
    public class DeleteBookTest : BaseTest
    {
        private BookStoreDto _bookStoreTestData = null!;

        [SetUp]
        public void SetUpBookStoreData()
        {
            var bookStoreDataPath = PathUtils.GetTestDataFilePath("bookstoredata.json");
            _bookStoreTestData = JsonUtils.ReadJsonFile<BookStoreDto>(bookStoreDataPath);
        }

        [Test]
        public async Task DeleteBookSuccessfully()
        {
            var expectedUserId = TestData.Account.UserId;
            var expectedUsername = TestData.Account.Username;
            var expectedIsbn = _bookStoreTestData.DeleteBook.Isbn;

            await EnsureBookExistsInCollection(expectedUserId, expectedIsbn);
            var deleteBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                Isbn = expectedIsbn
            };
            var deleteBookStopwatch = Stopwatch.StartNew();
            var deleteBookResponse = await BookStoreService.DeleteBookFromCollectionAsync(deleteBookRequest,Token);

            deleteBookStopwatch.Stop();
            ApiAssertions.VerifyStatusCode(deleteBookResponse, HttpStatusCode.NoContent);
            ReportLog.LogResponse("DELETE BOOK FROM COLLECTION",deleteBookResponse,deleteBookStopwatch.ElapsedMilliseconds);
            ReportLog.Info($"Verified delete book response successfully. Deleted ISBN: {expectedIsbn}");
            var getUserStopwatch = Stopwatch.StartNew();
            var getUserResponse = await AccountService.GetUserByIdAsync(expectedUserId,Token);
            getUserStopwatch.Stop();
            var actualUser = JsonConvert.DeserializeObject<AccountResponse>(getUserResponse.Content!);

            ApiAssertions.VerifyStatusCode(getUserResponse, HttpStatusCode.OK);
            ApiAssertions.VerifyResponseContentIsNotEmpty(getUserResponse);
            BookStoreAssertions.VerifyUserInformation(
                actualUser,
                expectedUserId,
                expectedUsername
            );

            BookStoreAssertions.VerifyBookDoesNotExistInCollection(
                actualUser!.Books,
                expectedIsbn
            );
            ReportLog.LogResponse("GET USER AFTER DELETE BOOK",getUserResponse,getUserStopwatch.ElapsedMilliseconds);
            ReportLog.Info($"Verified deleted book no longer exists in user's collection successfully. ISBN: {expectedIsbn}");
            ReportLog.LogBooks(actualUser!.Books);
        }

        [Test]
        public async Task DeleteBookUnsuccessfullyWithInvalidIsbn()
        {
            var expectedUserId = TestData.Account.UserId;
            var invalidIsbn = _bookStoreTestData.DeleteBook.InvalidIsbn;

            var deleteBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                Isbn = invalidIsbn
            };
            var stopwatch = Stopwatch.StartNew();
            var response = await BookStoreService.DeleteBookFromCollectionAsync(deleteBookRequest,Token);
            stopwatch.Stop();
            ApiAssertions.VerifyStatusCodeIsOneOf(
                response,
                HttpStatusCode.BadRequest,
                HttpStatusCode.NotFound
            );
            ApiAssertions.VerifyResponseContentIsNotEmpty(response);
            ReportLog.LogResponse("DELETE BOOK WITH INVALID ISBN",response,stopwatch.ElapsedMilliseconds);
            ReportLog.Info($"Verified cannot delete book with invalid ISBN. ISBN: {invalidIsbn}");
        }

        [Test]
        public async Task DeleteBookFromUnsuccessfullyWithInvalidToken()
        {
            var expectedUserId = TestData.Account.UserId;
            var expectedIsbn = _bookStoreTestData.DeleteBook.Isbn;
            var invalidToken = TestData.InvalidData.InvalidToken;

            var deleteBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                Isbn = expectedIsbn
            };
            var stopwatch = Stopwatch.StartNew();
            var response = await BookStoreService.DeleteBookFromCollectionAsync(deleteBookRequest,invalidToken);
            stopwatch.Stop();
            ApiAssertions.VerifyStatusCode(response, HttpStatusCode.Unauthorized);
            ApiAssertions.VerifyResponseContentIsNotEmpty(response);
            ReportLog.LogResponse("DELETE BOOK WITH INVALID TOKEN",response,stopwatch.ElapsedMilliseconds);
            ReportLog.Info("Verified cannot delete book with invalid token.");
        }

        private async Task EnsureBookExistsInCollection(string userId, string isbn)
        {
            var getUserResponse = await AccountService.GetUserByIdAsync(userId, Token);
            var actualUser = JsonConvert.DeserializeObject<AccountResponse>(getUserResponse.Content!);
            ApiAssertions.VerifyStatusCode(getUserResponse, HttpStatusCode.OK);
            ApiAssertions.VerifyResponseContentIsNotEmpty(getUserResponse);
            var isBookExisting = actualUser!.Books.Any(book => book.Isbn == isbn);

            if (isBookExisting)
            {
                ReportLog.Info($"Pre-condition passed: Book already exists in collection. ISBN: {isbn}");
                return;
            }

            var addBookRequest = new BookStoreRequest
            {
                UserId = userId,
                CollectionOfIsbns = new List<IsbnDto>
                {
                    new IsbnDto
                    {
                        Isbn = isbn
                    }
                }
            };

            var addBookResponse = await BookStoreService.AddBookToCollectionAsync(addBookRequest,Token);
            ApiAssertions.VerifyStatusCode(addBookResponse, HttpStatusCode.Created);
            ApiAssertions.VerifyResponseContentIsNotEmpty(addBookResponse);
            ReportLog.Info($"Pre-condition created: Book was added before deleting. ISBN: {isbn}");
        }
    }
}