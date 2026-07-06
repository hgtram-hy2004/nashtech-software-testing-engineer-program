using System.Diagnostics;
using System.Net;
using HuynhHoangTram.Core.Assertions;
using HuynhHoangTram.Core.Report;
using HuynhHoangTram.Models.Response;
using HuynhHoangTram.Test.Assertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HuynhHoangTram.Test
{
    [TestFixture]
    [Category("AccountAPI")]
    public class AccountTest : BaseTest
    {
        [Test]
        public async Task GetUserSuccessfully()
        {
            var expectedUserId = TestData.Account.UserId;
            var expectedUsername = TestData.Account.Username;
            var stopwatch = Stopwatch.StartNew();
            var response = await AccountService.GetUserByIdAsync(
                expectedUserId,
                Token
            );
            stopwatch.Stop();
            var actualUser = JsonConvert.DeserializeObject<AccountResponse>(response.Content!);

            ApiAssertions.VerifyStatusCode(response, HttpStatusCode.OK);
            ApiAssertions.VerifyResponseContentIsNotEmpty(response);
            BookStoreAssertions.VerifyUserInformation(
                actualUser,
                expectedUserId,
                expectedUsername
            );

            ReportLog.LogResponse("GET USER",response,stopwatch.ElapsedMilliseconds);
            if (actualUser!.Books != null && actualUser.Books.Any())
            {
                ReportLog.Info($"This account has {actualUser.Books.Count} book(s) in collection.");
                ReportLog.LogBooks(actualUser.Books);
            }
            else
            {
                ReportLog.Info("This account does not have any books in collection.");
            }
        }

        [Test]
        public async Task GetUserUnsuccessfully()
        {
            var invalidUserId = TestData.InvalidData.InvalidUserId;
            var stopwatch = Stopwatch.StartNew();
            var response = await AccountService.GetUserByIdAsync(invalidUserId,Token);
            stopwatch.Stop();
            ApiAssertions.VerifyStatusCodeIsOneOf(
                response,
                HttpStatusCode.Unauthorized,
                HttpStatusCode.NotFound
            );

            ApiAssertions.VerifyResponseContentIsNotEmpty(response);
            ReportLog.LogResponse("GET USER WITH INVALID USER ID",response,stopwatch.ElapsedMilliseconds);
        }
    }
}