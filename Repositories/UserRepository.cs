using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Bmwadforth.Common.Exceptions;
using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Bmwadforth.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly IAuthenticationService _authenticationService;
    
    public UserRepository(DatabaseContext databaseContext, IAuthenticationService authenticationService)
    {
        _databaseContext = databaseContext;
        _authenticationService = authenticationService;
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
    
    public async Task<(User, string)> LoginUser(string username, string password)
    {
        var user = await GetUserByUsername(username);
        var passwordMatches = _authenticationService.ValidateHash(password, user.Password);

        if (!passwordMatches)
        {
            throw new UserAuthenticationException("user is not authorized");
        }

        var claims = new List<Claim>()
        {
            new("user", user.UserId.ToString())
        };

        var token = _authenticationService.GenerateToken(claims);

        return (user ,new JwtSecurityTokenHandler().WriteToken(token));
    }
    
    public async Task<int> CreateUser(string username, string password)
    {
        var newUser = new User
        {
            Username = username,
            Password = _authenticationService.HashPassword(password)
        };

        _databaseContext.Users.Add(newUser);
        await _databaseContext.SaveChangesAsync();

        return newUser.UserId;
    }
}