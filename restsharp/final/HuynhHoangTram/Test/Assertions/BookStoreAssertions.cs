using HuynhHoangTram.Models;
using HuynhHoangTram.Models.Response;
using NUnit.Framework;

namespace HuynhHoangTram.Test.Assertions
{
    public static class BookStoreAssertions
    {
        public static void VerifyUserInformation(
            AccountResponse? actualUser,
            string expectedUserId,
            string expectedUsername
        )
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualUser, Is.Not.Null);
                Assert.That(actualUser!.UserId, Is.EqualTo(expectedUserId));
                Assert.That(actualUser.Username, Is.EqualTo(expectedUsername));
                Assert.That(actualUser.Books, Is.Not.Null);
            });
        }

        public static void VerifyBookExistsInCollection(
            List<BookResponse> books,
            string expectedIsbn
        )
        {
            Assert.That(
                books.Any(book => book.Isbn == expectedIsbn),
                Is.True,
                $"Expected book does not exist in user's collection. ISBN: {expectedIsbn}"
            );
        }

        public static void VerifyBookDoesNotExistInCollection(
            List<BookResponse> books,
            string expectedIsbn
        )
        {
            Assert.That(
                books.Any(book => book.Isbn == expectedIsbn),
                Is.False,
                $"Book still exists in user's collection. ISBN: {expectedIsbn}"
            );
        }

        public static void VerifyBooksAreCorrect(
            List<BookResponse> actualBooks,
            List<ExpectedBookData> expectedBooks
        )
        {
            Assert.That(actualBooks.Count, Is.EqualTo(expectedBooks.Count));

            foreach (var expectedBook in expectedBooks)
            {
                Assert.That(
                    actualBooks.Any(actualBook =>
                        actualBook.Isbn == expectedBook.Isbn &&
                        actualBook.Title == expectedBook.Title),
                    Is.True,
                    $"Expected book not found. ISBN: {expectedBook.Isbn}, Title: {expectedBook.Title}"
                );
            }
        }
        public static void VerifyBookStoreResponseContainsIsbn(
            BookStoreResponse? actualResponse,
            string expectedIsbn
        )
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualResponse, Is.Not.Null);
                Assert.That(actualResponse!.Books, Is.Not.Null);
                Assert.That(actualResponse.Books.Count, Is.EqualTo(1));

                Assert.That(
                    actualResponse.Books.Any(book => book.Isbn == expectedIsbn),
                    Is.True,
                    $"Expected ISBN was not returned in Book Store response. ISBN: {expectedIsbn}"
                );
            });
        }
    }
}