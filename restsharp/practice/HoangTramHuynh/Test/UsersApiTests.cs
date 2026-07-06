using System.Diagnostics;
using System.Net;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.DataObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace HoangTramHuynh.Test
{
    [TestFixture]
    [Category("Users API")]
    public class UsersApiTests : BaseTest
    {
        [Test]
        public async Task GetUsersSuccessfully()
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await UserService.GetUsersAsync();
            stopwatch.Stop();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(10000));
                Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            });

            var responseArray = JArray.Parse(response.Content!);
            var actualUsers = JsonConvert.DeserializeObject<List<UserResponseDto>>(response.Content!);
            Assert.Multiple(() =>
            {
                Assert.That(responseArray.Count, Is.GreaterThan(0));
                Assert.That(responseArray[0]["id"], Is.Not.Null);
                Assert.That(responseArray[0]["name"], Is.Not.Null);
                Assert.That(responseArray[0]["email"], Is.Not.Null);
                Assert.That(responseArray[0]["gender"], Is.Not.Null);
                Assert.That(responseArray[0]["status"], Is.Not.Null);

                Assert.That(actualUsers, Is.Not.Null);
                Assert.That(actualUsers, Is.Not.Empty);

                Assert.That(actualUsers![0].Id, Is.GreaterThan(0));
                Assert.That(actualUsers[0].Name, Is.Not.Null.And.Not.Empty);
                Assert.That(actualUsers[0].Email, Is.Not.Null.And.Not.Empty);
                Assert.That(actualUsers[0].Gender, Is.Not.Null.And.Not.Empty);
                Assert.That(actualUsers[0].Status, Is.Not.Null.And.Not.Empty);
            });
            ReportLog.LogResponse("GET USERS", response, stopwatch.ElapsedMilliseconds);
        }

        [Test]
        public async Task GetUserByIdSuccessfully()
        {
            var userId = await UserTestHelperInstance.CreateUserAndReturnIdAsync();
            var stopwatch = Stopwatch.StartNew();
            var response = await UserTestHelperInstance.GetUserByIdWithRetryAsync(userId);
            stopwatch.Stop();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(2000));
                Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            });

            var responseBody = JObject.Parse(response.Content!);
            Assert.Multiple(() =>
            {
                Assert.That(responseBody["id"], Is.Not.Null);
                Assert.That(responseBody["name"], Is.Not.Null);
                Assert.That(responseBody["email"], Is.Not.Null);
                Assert.That(responseBody["gender"], Is.Not.Null);
                Assert.That(responseBody["status"], Is.Not.Null);

                Assert.That(responseBody["id"]!.ToString(), Is.EqualTo(userId.ToString()));
            });
            ReportLog.LogResponse("GET USER BY ID", response, stopwatch.ElapsedMilliseconds);
        }

        [Test]
        public async Task CreateUserSuccessfully()
        {
            var expectedUser = UserTestHelperInstance.BuildValidUser();
            var stopwatch = Stopwatch.StartNew();
            var response = await UserService.CreateUserAsync(expectedUser);
            stopwatch.Stop();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(2000));
                Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            });

            var responseBody = JObject.Parse(response.Content!);
            Assert.Multiple(() =>
            {
                Assert.That(responseBody["id"], Is.Not.Null);
                Assert.That(responseBody["name"], Is.Not.Null);
                Assert.That(responseBody["email"], Is.Not.Null);
                Assert.That(responseBody["gender"], Is.Not.Null);
                Assert.That(responseBody["status"], Is.Not.Null);

                Assert.That(responseBody["name"]!.ToString(), Is.EqualTo(expectedUser.Name));
                Assert.That(responseBody["email"]!.ToString(), Is.EqualTo(expectedUser.Email));
                Assert.That(responseBody["gender"]!.ToString(), Is.EqualTo(expectedUser.Gender));
                Assert.That(responseBody["status"]!.ToString(), Is.EqualTo(expectedUser.Status));
            });
            ReportLog.LogResponse("CREATE USER", response, stopwatch.ElapsedMilliseconds);
        }

        [Test]
        public async Task UpdateUserSuccessfully()
        {
            var userId = await UserTestHelperInstance.CreateUserAndReturnIdAsync();
            var expectedUser = UserTestHelperInstance.BuildUpdatedUser();
            var stopwatch = Stopwatch.StartNew();
            var response = await UserService.UpdateUserByPutAsync(userId, expectedUser);
            stopwatch.Stop();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(2000));
                Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            });

            var responseBody = JObject.Parse(response.Content!);
            Assert.Multiple(() =>
            {
                Assert.That(responseBody["id"], Is.Not.Null);
                Assert.That(responseBody["name"], Is.Not.Null);
                Assert.That(responseBody["email"], Is.Not.Null);
                Assert.That(responseBody["gender"], Is.Not.Null);
                Assert.That(responseBody["status"], Is.Not.Null);

                Assert.That(responseBody["id"]!.ToString(), Is.EqualTo(userId.ToString()));
                Assert.That(responseBody["name"]!.ToString(), Is.EqualTo(expectedUser.Name));
                Assert.That(responseBody["email"]!.ToString(), Is.EqualTo(expectedUser.Email));
                Assert.That(responseBody["gender"]!.ToString(), Is.EqualTo(expectedUser.Gender));
                Assert.That(responseBody["status"]!.ToString(), Is.EqualTo(expectedUser.Status));
            });
            ReportLog.LogResponse("UPDATE USER BY PUT", response, stopwatch.ElapsedMilliseconds);
        }

        [Test]
        public async Task DeleteUserSuccessfully()
        {
            var userId = await UserTestHelperInstance.CreateUserAndReturnIdAsync();
            var stopwatch = Stopwatch.StartNew();
            var response = await UserService.DeleteUserAsync(userId);
            stopwatch.Stop();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
                Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(2000));
            });
            ReportLog.LogResponse("DELETE USER", response, stopwatch.ElapsedMilliseconds);
        }

    }
}