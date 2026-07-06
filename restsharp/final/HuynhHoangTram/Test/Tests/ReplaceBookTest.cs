using System.Diagnostics;
using System.Net;
using HuynhHoangTram.Core.Report;
using HuynhHoangTram.Core.Services;
using HuynhHoangTram.Core.Utilities;
using HuynhHoangTram.Models;
using HuynhHoangTram.Models.Request;
using HuynhHoangTram.Models.Response;
using HuynhHoangTram.Core.Assertions;
using HuynhHoangTram.Test.Assertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HuynhHoangTram.Test
{
    [TestFixture]
    [Category("ReplaceBookAPI")]
    public class ReplaceBookTest : BaseTest
    {
        private BookStoreDto _bookStoreTestData = null!;

        [SetUp]
        public void SetUpBookStoreData()
        {
            var bookStoreDataPath = PathUtils.GetTestDataFilePath("bookstoredata.json");
            _bookStoreTestData = JsonUtils.ReadJsonFile<BookStoreDto>(bookStoreDataPath);
        }

       [Test]
        public async Task ReplaceBookSuccessfully()
        {
            var expectedUserId = TestData.Account.UserId;
            var expectedUsername = TestData.Account.Username;
            var oldIsbn = _bookStoreTestData.ReplaceBook.OldIsbn;
            var newIsbn = _bookStoreTestData.ReplaceBook.NewIsbn;

            await EnsureBookExistsInCollection(expectedUserId, oldIsbn);
            await EnsureBookDoesNotExistInCollection(expectedUserId, newIsbn);

            ReportLog.Info($"Replace Request - Old ISBN in URL: {oldIsbn}");
            ReportLog.Info($"Replace Request - New ISBN in Body: {newIsbn}");

            var replaceBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                Isbn = newIsbn
            };

            var stopwatch = Stopwatch.StartNew();

            var replaceBookResponse = await BookStoreService.ReplaceBookInCollectionAsync(
                oldIsbn,
                replaceBookRequest,
                Token
            );

            stopwatch.Stop();

            var actualReplaceBookUser = JsonConvert.DeserializeObject<AccountResponse>(
                replaceBookResponse.Content!
            );

            ApiAssertions.VerifyStatusCode(replaceBookResponse, HttpStatusCode.OK);
            ApiAssertions.VerifyResponseContentIsNotEmpty(replaceBookResponse);

            JsonSchemaAssertions.VerifyJsonSchema(
                replaceBookResponse.Content!,
                "replacebookschema.json"
            );

            BookStoreAssertions.VerifyUserInformation(
                actualReplaceBookUser,
                expectedUserId,
                expectedUsername
            );

            BookStoreAssertions.VerifyBookDoesNotExistInCollection(
                actualReplaceBookUser!.Books,
                oldIsbn
            );

            BookStoreAssertions.VerifyBookExistsInCollection(
                actualReplaceBookUser.Books,
                newIsbn
            );

            ReportLog.LogResponse(
                "REPLACE BOOK IN COLLECTION",
                replaceBookResponse,
                stopwatch.ElapsedMilliseconds
            );

            ReportLog.Info(
                $"Verified replace response successfully. Old ISBN was removed: {oldIsbn}, New ISBN was added: {newIsbn}"
            );

            ReportLog.LogBooks(actualReplaceBookUser.Books);

            var getUserStopwatch = Stopwatch.StartNew();

            var getUserResponse = await AccountService.GetUserByIdAsync(
                expectedUserId,
                Token
            );

            getUserStopwatch.Stop();

            var actualUser = JsonConvert.DeserializeObject<AccountResponse>(
                getUserResponse.Content!
            );

            ApiAssertions.VerifyStatusCode(getUserResponse, HttpStatusCode.OK);
            ApiAssertions.VerifyResponseContentIsNotEmpty(getUserResponse);

            BookStoreAssertions.VerifyUserInformation(
                actualUser,
                expectedUserId,
                expectedUsername
            );

            BookStoreAssertions.VerifyBookDoesNotExistInCollection(
                actualUser!.Books,
                oldIsbn
            );

            BookStoreAssertions.VerifyBookExistsInCollection(
                actualUser.Books,
                newIsbn
            );

            var replacedBook = actualUser.Books.First(book => book.Isbn == newIsbn);

            ReportLog.LogResponse(
                "GET USER AFTER REPLACE BOOK",
                getUserResponse,
                getUserStopwatch.ElapsedMilliseconds
            );

            ReportLog.Info(
                $"Verified book was replaced successfully in user's collection. Old ISBN removed: {oldIsbn}, New ISBN: {replacedBook.Isbn}, Title: {replacedBook.Title}"
            );

            ReportLog.LogBooks(actualUser.Books);
        }

        [Test]
        public async Task ReplaceBookUnsuccessfullyWithInvalidIsbn()
        {
            var expectedUserId = TestData.Account.UserId;
            var oldIsbn = _bookStoreTestData.ReplaceBook.OldIsbn;
            var invalidIsbn = _bookStoreTestData.ReplaceBook.InvalidIsbn;
            await EnsureBookExistsInCollection(expectedUserId, oldIsbn);
            var replaceBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                Isbn = invalidIsbn
            };
            var stopwatch = Stopwatch.StartNew();
            var response = await BookStoreService.ReplaceBookInCollectionAsync(oldIsbn,replaceBookRequest,Token);
            stopwatch.Stop();
            ApiAssertions.VerifyStatusCodeIsOneOf(
                response,
                HttpStatusCode.BadRequest,
                HttpStatusCode.NotFound
            );
            ApiAssertions.VerifyResponseContentIsNotEmpty(response);
            ReportLog.LogResponse("REPLACE BOOK WITH INVALID ISBN",response,stopwatch.ElapsedMilliseconds);
            ReportLog.Info($"Verified cannot replace book with invalid ISBN. Invalid ISBN: {invalidIsbn}");
        }

        [Test]
        public async Task ReplaceBookUnsuccessfullyWithInvalidToken()
        {
            var expectedUserId = TestData.Account.UserId;
            var oldIsbn = _bookStoreTestData.ReplaceBook.OldIsbn;
            var newIsbn = _bookStoreTestData.ReplaceBook.NewIsbn;
            var invalidToken = TestData.InvalidData.InvalidToken;

            var replaceBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                Isbn = newIsbn
            };

            var stopwatch = Stopwatch.StartNew();
            var response = await BookStoreService.ReplaceBookInCollectionAsync(
                oldIsbn,
                replaceBookRequest,
                invalidToken
            );
            stopwatch.Stop();
            ApiAssertions.VerifyStatusCode(response, HttpStatusCode.Unauthorized);
            ApiAssertions.VerifyResponseContentIsNotEmpty(response);
            ReportLog.LogResponse("REPLACE BOOK WITH INVALID TOKEN",response,stopwatch.ElapsedMilliseconds);
            ReportLog.Info("Verified cannot replace book with invalid token.");
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
            ReportLog.Info($"Pre-condition created: Book was added into collection. ISBN: {isbn}");
        }

        private async Task EnsureBookDoesNotExistInCollection(string userId, string isbn)
        {
            var getUserResponse = await AccountService.GetUserByIdAsync(userId, Token);
            var actualUser = JsonConvert.DeserializeObject<AccountResponse>(getUserResponse.Content!);
            ApiAssertions.VerifyStatusCode(getUserResponse, HttpStatusCode.OK);
            ApiAssertions.VerifyResponseContentIsNotEmpty(getUserResponse);
            var isBookExisting = actualUser!.Books.Any(book => book.Isbn == isbn);
            if (!isBookExisting)
            {
                ReportLog.Info($"Pre-condition passed: New book does not exist in collection. ISBN: {isbn}");
                return;
            }
            var deleteBookRequest = new BookStoreRequest
            {
                UserId = userId,
                Isbn = isbn
            };

            var deleteBookResponse = await BookStoreService.DeleteBookFromCollectionAsync(deleteBookRequest,Token);
            ApiAssertions.VerifyStatusCode(deleteBookResponse, HttpStatusCode.NoContent);
            ReportLog.Info($"Pre-condition created: New book was removed before replacing. ISBN: {isbn}");
        }
    }
}