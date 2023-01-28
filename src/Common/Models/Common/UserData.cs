namespace BlogWebsite.Common.Models.Common
{
    public class UserData
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTimeOffset LoggedInSince { get; set; }
    }
}
