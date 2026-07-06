using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuynhHoangTram.Models
{
    public class BookStoreDto
    {
        public AddBookData AddBook { get; set; } = null!;
        public DeleteBookData DeleteBook { get; set; } = null!;
        public ReplaceBookData ReplaceBook { get; set; } = null!;
    }

    public class AddBookData
    {
        public string Isbn { get; set; } = string.Empty;
        public string DuplicateIsbn { get; set; } = string.Empty;
        public string InvalidIsbn { get; set; } = string.Empty;
    }

    public class DeleteBookData
    {
        public string Isbn { get; set; } = string.Empty;
        public string InvalidIsbn { get; set; } = string.Empty;
    }
    public class ReplaceBookData
    {
        public string OldIsbn { get; set; } = string.Empty;
        public string NewIsbn { get; set; } = string.Empty;
        public string InvalidIsbn { get; set; } = string.Empty;
    }
}