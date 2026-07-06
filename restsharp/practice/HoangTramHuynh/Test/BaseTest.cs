using HoangTramHuynh.Core.API;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.Core.Utilities;
using HoangTramHuynh.Core.Services;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace HoangTramHuynh.Test
{
    public class BaseTest
    {
        protected ApiClient ApiClient = null!;
        protected UserService UserService = null!;
        protected UserTestHelper UserTestHelperInstance = null!;
        public BaseTest()
        {
            var baseUrl = ConfigurationUtils.GetConfigurationByKey("application:url");
            var token = ConfigurationUtils.GetConfigurationByKey("application:token");
            ApiClient = new ApiClient(baseUrl);
            UserService = new UserService(ApiClient, token);
            UserTestHelperInstance = new UserTestHelper(UserService);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ExtentReport.InitReport();
        }

        [SetUp]
        public void BeforeTest()
        {
            ExtentReport.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
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

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReport.FlushReport();
        }
    }
}