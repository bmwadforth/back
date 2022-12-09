
using BlogWebsite.Common.Models;

namespace BlogWebsite.Common.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserById(int id);
    Task<User> GetUserByUsername(string username);
    Task<int> CreateUser(string username, string password);
    Task<(User, string)> LoginUser(string username, string password);
}