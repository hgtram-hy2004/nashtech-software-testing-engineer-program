using HoangTramHuynh.DataObject;

namespace HoangTramHuynh.Contexts
{
    public class DeleteBookContext
    {
        public LoginData LoginData { get; set; } = new LoginData();
        public BookData BookData { get; set; } = new BookData();

        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}