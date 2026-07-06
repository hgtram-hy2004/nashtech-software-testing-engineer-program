using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuynhHoangTram.Models
{
    public class AccountDto
    {
        public AccountData Account { get; set; } = null!;
        public BookData Book { get; set; } = null!;
        public List<ExpectedBookData> ExpectedBooks { get; set; } = new();
        public InvalidData InvalidData { get; set; } = null!;
    }

    public class AccountData
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class ExpectedBookData
    {
        public string Isbn { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }

    public class BookData
    {
        public string ValidIsbn { get; set; } = string.Empty;
        public string OldIsbn { get; set; } = string.Empty;
        public string NewIsbn { get; set; } = string.Empty;
    }

    public class InvalidData
    {
        public string InvalidUserId { get; set; } = string.Empty;
        public string InvalidIsbn { get; set; } = string.Empty;
        public string InvalidToken { get; set; } = string.Empty;
    }
}