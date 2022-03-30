using Bmwadforth.Common.Exceptions;
using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Bmwadforth.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;
    
    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user == null) throw new NotFoundException($"user with id: {id} not found");

        return user;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) throw new NotFoundException($"user with username: {username} not found");

        return user;
    }
    
    public async Task<bool> LoginUser(string username, string password)
    {
        var user = await GetUserByUsername(username);
        var passwordMatches = ValidateHash(password, user.Password);

        if (!passwordMatches)
        {
            throw new UserAuthenticationException("user is not authorized");
        }

        return true;
    }
    
    public async Task<int> CreateUser(string username, string password)
    {
        var newUser = new User
        {
            Username = username,
            Password = HashPassword(password)
        };

        _databaseContext.Users.Add(newUser);
        await _databaseContext.SaveChangesAsync();

        return newUser.UserId;
    }

    private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    
    private bool ValidateHash(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}