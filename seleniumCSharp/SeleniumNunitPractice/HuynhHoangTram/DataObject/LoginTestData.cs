using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuynhHoangTram.DataObject
{
    public static class LoginTestData
    {
        public static IEnumerable<TestCaseData> InvalidLoginData()
        {
            yield return new TestCaseData("Admin2", "").SetName("LoginWithEmptyPassword");

            yield return new TestCaseData("", "qp$#tGu^").SetName("LoginWithEmptyUsername");

            yield return new TestCaseData("", "").SetName("LoginWithEmptyUsernameAndPassword");

            yield return new TestCaseData("Admin1", "qp$#tGu3").SetName("LoginWithInvalidUsernameAndPassword");
        }
    }
}