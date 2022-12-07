using BlogWebsite.Common.Exceptions;
using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogWebsite.Common.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly IJwtAuthenticationService _jwtAuthenticationService;

    public UserRepository(DatabaseContext databaseContext, IJwtAuthenticationService jwtAuthenticationService)
    {
        _databaseContext = databaseContext;
        _jwtAuthenticationService = jwtAuthenticationService;
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
        var passwordMatches = _jwtAuthenticationService.ValidateHash(password, user.Password);

        if (!passwordMatches)
        {
            throw new AuthenticationException("user is not authorized");
        }

        var claims = new List<Claim>()
        {
            new("user", user.UserId.ToString())
        };

        var token = _jwtAuthenticationService.GenerateToken(claims);

        return (user, new JwtSecurityTokenHandler().WriteToken(token));
    }

    public async Task<int> CreateUser(string username, string password)
    {
        var newUser = new User
        {
            Username = username,
            Password = _jwtAuthenticationService.HashPassword(password)
        };

        _databaseContext.Users.Add(newUser);
        await _databaseContext.SaveChangesAsync();

        return newUser.UserId;
    }
}