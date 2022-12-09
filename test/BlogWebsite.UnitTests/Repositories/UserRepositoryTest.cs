using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlogWebsite.Common.Exceptions;
using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Repositories;
using BlogWebsite.UnitTests.Helpers;
using NSubstitute;
using Xunit;

namespace BlogWebsite.UnitTests.Repositories;

public class UserRepositoryTest : IClassFixture<DatabaseContextFixture>
{
    private readonly IUserRepository _sut;
    private readonly IJwtAuthenticationService _dep;
    
    public UserRepositoryTest(DatabaseContextFixture fixture)
    {
        var jwtAuthenticationService = Substitute.For<IJwtAuthenticationService>();
        _dep = jwtAuthenticationService;

        _sut = new UserRepository(fixture.DatabaseContext, jwtAuthenticationService);
    }
    
    [Fact]
    public async void UserRepository_ReturnsUser()
    {
        var result = await _sut.GetUserById(1);
        
        Assert.Equal("test", result.Username);
        Assert.Equal(1, result.UserId);
    }
    
    [Fact]
    public async void UserRepository_LogsInUser()
    {
        _dep.ValidateHash("my_password", "$2a$11$1Fy9dWPjk1Neb7l5XLgkNeY.ybt5jZO9Xw0wZ2EohWRmeg8gKnWh.").Returns(true);
        _dep.GenerateToken(default).ReturnsForAnyArgs(new JwtSecurityToken());
        var (user, token) = await _sut.LoginUser("test", "my_password");
        
        Assert.Equal("test", user.Username);
        Assert.Equal("eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.e30.", token);
    }
    
    [Fact]
    public async void UserRepository_ThrowsExceptionOnInvalidLogsInUser()
    {
        _dep.ValidateHash("my_password", "$2a$11$1Fy9dWPjk1Neb7l5XLgkNeY.ybt5jZO9Xw0wZ2EohWRmeg8gKnWh.").Returns(false);
        await Assert.ThrowsAsync<AuthenticationException>(async () => await _sut.LoginUser("test", "my_password"));
    }
}