
using Bmwadforth.Common.Models;

namespace Bmwadforth.Common.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserById(int id);
    Task<User> GetUserByUsername(string username);
    Task<int> CreateUser(string username, string password);
    Task<bool> LoginUser(string username, string password);
}