using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoangTramHuynh.DataObjects
{
    public class TestDataDto
    {
        public UserData ValidUser { get; set; } = null!;
        public UserData SchemaTestUser { get; set; } = null!;
        public UserData UpdatedUser { get; set; } = null!;
    }

    public class UserData
    {
        public string Name { get; set; } = string.Empty;
        public string EmailPrefix { get; set; } = string.Empty;
        public string EmailDomain { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}