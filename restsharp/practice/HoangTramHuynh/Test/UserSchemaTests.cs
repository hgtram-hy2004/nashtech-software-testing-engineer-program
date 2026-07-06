using System.Diagnostics;
using System.Net;
using HoangTramHuynh.Core.Report;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using HoangTramHuynh.Core.Utilities;
using NUnit.Framework;

namespace HoangTramHuynh.Test
{
    [TestFixture]
    [Category("Users API Schemas")]
    public class UserSchemaTests : BaseTest
    {
        [Test]
        public async Task UpdateUserReturnJsonSchema()
        {
            var userId = await UserTestHelperInstance.CreateUserAndReturnIdAsync();
            var expectedUser = UserTestHelperInstance.BuildSchemaTestUser();
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
            });

            var schemaPath = PathUtils.GetSchemaFilePath("UpdateUserSchema.json");
            var schemaJson = File.ReadAllText(schemaPath);
            var schema = JSchema.Parse(schemaJson);
            var isValidSchema = responseBody.IsValid(schema, out IList<string> errors);
            Assert.That(
                isValidSchema,
                Is.True,
                $"Response body should match JSON schema. Errors: {string.Join(", ", errors)}"
            );
            ReportLog.LogResponse("UPDATE USER SCHEMA", response, stopwatch.ElapsedMilliseconds);
        }
    }
}