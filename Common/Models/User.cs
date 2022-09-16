namespace Bmwadforth.Common.Models;

public class User : BaseEntity
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}