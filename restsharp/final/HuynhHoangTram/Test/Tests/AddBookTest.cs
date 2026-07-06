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
    [Category("AddBookAPI")]
    public class AddBookTest : BaseTest
    {
        private BookStoreDto _bookStoreData = null!;
        [SetUp]
        public void SetUpBookStoreData()
        {
            var bookStoreDataPath = PathUtils.GetTestDataFilePath("bookstoredata.json");
            _bookStoreData = JsonUtils.ReadJsonFile<BookStoreDto>(bookStoreDataPath);
        }
        [Test]
        public async Task AddBookSuccessfully()
        {
            var expectedUserId = TestData.Account.UserId;
            var expectedUsername = TestData.Account.Username;
            var expectedIsbn = _bookStoreData.AddBook.Isbn;

            var addBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                CollectionOfIsbns = new List<IsbnDto>
                {
                    new IsbnDto
                    {
                        Isbn = expectedIsbn
                    }
                }
            };

            var addBookStopwatch = Stopwatch.StartNew();
            var addBookResponse = await BookStoreService.AddBookToCollectionAsync(addBookRequest,Token);
            addBookStopwatch.Stop();
            var actualAddBookResponse = JsonConvert.DeserializeObject<BookStoreResponse>(addBookResponse.Content!);
            ApiAssertions.VerifyStatusCode(addBookResponse, HttpStatusCode.Created);
            ApiAssertions.VerifyResponseContentIsNotEmpty(addBookResponse);

            BookStoreAssertions.VerifyBookStoreResponseContainsIsbn(
                actualAddBookResponse,
                expectedIsbn
            );
            ReportLog.LogResponse("ADD BOOK TO COLLECTION",addBookResponse,addBookStopwatch.ElapsedMilliseconds);
            ReportLog.Info($"Verified add book response successfully. Added ISBN: {expectedIsbn}");
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

            BookStoreAssertions.VerifyBookExistsInCollection(
                actualUser!.Books,
                expectedIsbn
            );
            var addedBook = actualUser?.Books.FirstOrDefault(book => book.Isbn == expectedIsbn);
            ReportLog.LogResponse("GET USER AFTER ADD BOOK",getUserResponse,getUserStopwatch.ElapsedMilliseconds);
            ReportLog.Info($"Verified added book exists in collection successfully. ISBN: {addedBook!.Isbn}, Title: {addedBook.Title}");
            ReportLog.LogBooks(actualUser!.Books);
        }
        [Test]
        public async Task AddBookUnsuccessfullyWithInvalidIsbn()
        {
            var expectedUserId = TestData.Account.UserId;
            var invalidIsbn = _bookStoreData.AddBook.InvalidIsbn;
            var addBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                CollectionOfIsbns = new List<IsbnDto>
                {
                    new IsbnDto
                    {
                        Isbn = invalidIsbn
                    }
                }
            };

            var stopwatch = Stopwatch.StartNew();
            var response = await BookStoreService.AddBookToCollectionAsync(addBookRequest,Token);
            stopwatch.Stop();
            ApiAssertions.VerifyStatusCodeIsOneOf(
                response,
                HttpStatusCode.BadRequest,
                HttpStatusCode.NotFound
            );
            ApiAssertions.VerifyResponseContentIsNotEmpty(response);
            ReportLog.LogResponse("ADD BOOK WITH INVALID ISBN",response,stopwatch.ElapsedMilliseconds);
        }
        [Test]
        public async Task AddBookUnsuccessfullyWhenBookAlreadyExists()
        {
            var expectedUserId = TestData.Account.UserId;
            var existingIsbn = _bookStoreData.AddBook.DuplicateIsbn;
            var getUserBeforeResponse = await AccountService.GetUserByIdAsync(expectedUserId, Token);
            var actualUserBefore = JsonConvert.DeserializeObject<AccountResponse>(getUserBeforeResponse.Content!);
            var isBookAlreadyExisting = actualUserBefore!.Books.Any(book => book.Isbn == existingIsbn);

            if (!isBookAlreadyExisting)
            {
                var preconditionRequest = new BookStoreRequest
                {
                    UserId = expectedUserId,
                    CollectionOfIsbns = new List<IsbnDto>
                    {
                        new IsbnDto
                        {
                            Isbn = existingIsbn
                        }
                    }
                };
                var preconditionResponse = await BookStoreService.AddBookToCollectionAsync(preconditionRequest,Token);
                Assert.That(preconditionResponse.StatusCode,Is.EqualTo(HttpStatusCode.Created),"Pre-condition failed: Cannot add book before testing duplicate book case.");
            }

            var duplicateBookRequest = new BookStoreRequest
            {
                UserId = expectedUserId,
                CollectionOfIsbns = new List<IsbnDto>
                {
                    new IsbnDto
                    {
                        Isbn = existingIsbn
                    }
                }
            };
            var stopwatch = Stopwatch.StartNew();
            var response = await BookStoreService.AddBookToCollectionAsync(duplicateBookRequest,Token);
            stopwatch.Stop();
            ApiAssertions.VerifyStatusCode(response, HttpStatusCode.BadRequest);
            ApiAssertions.VerifyResponseContentIsNotEmpty(response);

            Assert.That(response.Content, Does.Contain("ISBN"));

            ReportLog.LogResponse("ADD EXISTING BOOK TO COLLECTION",response,stopwatch.ElapsedMilliseconds);
            ReportLog.Info($"Verified cannot add duplicate book. ISBN: {existingIsbn}");
        }
    }
}