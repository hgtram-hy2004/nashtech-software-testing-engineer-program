using System.Net;
using NUnit.Framework;
using RestSharp;

namespace HuynhHoangTram.Core.Assertions
{
    public static class ApiAssertions
    {
        public static void VerifyStatusCode(RestResponse response, HttpStatusCode expectedStatusCode)
        {
            Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
        }
        public static void VerifyStatusCodeIsOneOf(RestResponse response,params HttpStatusCode[] expectedStatusCodes)
        {
            Assert.That(
                expectedStatusCodes,
                Does.Contain(response.StatusCode),
                $"Expected status code should be one of: {string.Join(", ", expectedStatusCodes)}, but was: {response.StatusCode}"
            );
        }

        public static void VerifyResponseContentIsNotEmpty(RestResponse response)
        {
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
        }

        public static void VerifyResponseContentIsEmpty(RestResponse response)
        {
            Assert.That(response.Content, Is.Null.Or.Empty);
        }

        public static void VerifyResponseTimeLessThan(long actualResponseTime, long expectedResponseTime)
        {
            Assert.That(actualResponseTime, Is.LessThan(expectedResponseTime));
        }
    }
}