using HuynhHoangTram.Core.Report;
using HuynhHoangTram.Core.Utilities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace HuynhHoangTram.Core.Assertions
{
    public static class JsonSchemaAssertions
    {
        public static void VerifyJsonSchema(string responseContent, string schemaFileName)
        {
            var schemaPath = PathUtils.GetSchemaFilePath(schemaFileName);
            var schemaContent = File.ReadAllText(schemaPath);

            var schema = JSchema.Parse(schemaContent);
            var json = JToken.Parse(responseContent);

            var isValid = json.IsValid(schema, out IList<string> errorMessages);

            ReportLog.Info($"JSON Schema File: {schemaFileName}");
            ReportLog.Info($"JSON Schema Validation Result: {(isValid ? "Passed" : "Failed")}");

            if (!isValid)
            {
                foreach (var errorMessage in errorMessages)
                {
                    ReportLog.Info($"JSON Schema Error: {errorMessage}");
                }
            }

            Assert.That(
                isValid,
                Is.True,
                $"Response body does not match JSON schema: {schemaFileName}"
            );
        }
    }
}