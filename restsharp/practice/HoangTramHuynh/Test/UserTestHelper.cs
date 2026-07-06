using System.Net;
using HoangTramHuynh.Core.Utilities;
using HoangTramHuynh.DataObjects;
using HoangTramHuynh.Core.Services;
using Newtonsoft.Json;
using RestSharp;
using NUnit.Framework;

namespace HoangTramHuynh.Test
{
    public class UserTestHelper
    {
        private readonly UserService _userService;
        private readonly TestDataDto _userTestData;
        public UserTestHelper(UserService userService)
        {
            _userService = userService;
            var userTestDataPath = PathUtils.GetTestDataFilePath("users.json");
            _userTestData = JsonUtils.ReadJsonFile<TestDataDto>(userTestDataPath);
        }

        public async Task<int> CreateUserAndReturnIdAsync()
        {
            var user = BuildUserRequest(_userTestData.ValidUser);
            var response = await _userService.CreateUserAsync(user);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            });

            var actualUser = JsonConvert.DeserializeObject<UserResponseDto>(response.Content!);
            Assert.Multiple(() =>
            {
                Assert.That(actualUser, Is.Not.Null);
                Assert.That(actualUser!.Id, Is.GreaterThan(0));
            });
            return actualUser!.Id;
        }

        public async Task<RestResponse> GetUserByIdWithRetryAsync(int userId)
        {
            RestResponse response = null!;
            for (int retryTime = 1; retryTime <= 5; retryTime++)
            {
                response = await _userService.GetUserByIdAsync(userId);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response;
                }
                await Task.Delay(1000);
            }
            return response;
        }

        public UserRequestDto BuildValidUser()
        {
            return BuildUserRequest(_userTestData.ValidUser);
        }

        public UserRequestDto BuildSchemaTestUser()
        {
            return BuildUserRequest(_userTestData.SchemaTestUser);
        }

        public UserRequestDto BuildUpdatedUser()
        {
            return BuildUserRequest(_userTestData.UpdatedUser);
        }

        private static UserRequestDto BuildUserRequest(UserData userData)
        {
            return new UserRequestDto
            {
                Name = userData.Name,
                Email = GenerateUniqueEmail(userData.EmailPrefix, userData.EmailDomain),
                Gender = userData.Gender,
                Status = userData.Status
            };
        }

        private static string GenerateUniqueEmail(string prefix, string domain)
        {
            return $"{prefix}{Guid.NewGuid():N}@{domain}";
        }
        private static string GetProjectRootDirectory()
        {
            var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (currentDirectory != null && !currentDirectory.GetFiles("*.csproj").Any())
            {
                currentDirectory = currentDirectory.Parent;
            }
            if (currentDirectory == null)
            {
                throw new DirectoryNotFoundException("Project root directory was not found.");
            }
            return currentDirectory.FullName;
        }
    }
}