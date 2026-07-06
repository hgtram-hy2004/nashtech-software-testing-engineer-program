using HuynhHoangTram.Core.API;
using HuynhHoangTram.Core.Report;
using HuynhHoangTram.Core.Services;
using HuynhHoangTram.Core.Utilities;
using HuynhHoangTram.Models;
using HuynhHoangTram.Models.Request;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace HuynhHoangTram.Test
{
    public class BaseTest
    {
        protected APIClient ApiClient = null!;
        protected AccountService AccountService = null!;
        protected BookStoreService BookStoreService = null!;
        protected AccountDto TestData = null!;
        protected string Token = string.Empty;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ExtentReport.InitReport();
        }

        [SetUp]
        public async Task BaseSetUpAsync()
        {
            ExtentReport.CreateTest(TestContext.CurrentContext.Test.Name);

            var baseUrl = ConfigUtils.GetConfigurationByKey("application:url");

            ApiClient = new APIClient(baseUrl);
            AccountService = new AccountService(ApiClient);
            BookStoreService = new BookStoreService(ApiClient);

            var userDataPath = PathUtils.GetTestDataFilePath("userdata.json");
            TestData = JsonUtils.ReadJsonFile<AccountDto>(userDataPath);

            var accountRequest = new AccountRequest
            {
                userName = TestData.Account.Username,
                password = TestData.Account.Password
            };

            Token = await AccountService.GenerateTokenAsync(accountRequest);
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            try
            {
                if (status == TestStatus.Passed)
                {
                    ExtentReport.LogPass("Test passed");
                }
                else if (status == TestStatus.Failed)
                {
                    ExtentReport.LogFail($"Test failed: {message}");
                    ExtentReport.LogFail(stackTrace ?? string.Empty);
                }
                else
                {
                    ExtentReport.LogInfo($"Test status: {status}");
                }
            }
            finally
            {
                ExtentReport.ClearTest();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReport.FlushReport();
        }
    }
}