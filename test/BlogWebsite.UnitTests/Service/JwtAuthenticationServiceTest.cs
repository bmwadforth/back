using System.Security.Claims;
using BlogWebsite.Common.Configuration;
using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Service;
using BlogWebsite.UnitTests.Helpers;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace BlogWebsite.UnitTests.Service;
using Xunit;

public class JwtAuthenticationServiceTest
{
    private readonly IJwtAuthenticationService _sut;

    public JwtAuthenticationServiceTest()
    {
        var config = TestHelper.GetConfig();
        _sut = new JwtAuthenticationService(config);
    }
    
    [Fact]
    public async void JwtAuthenticationServiceTest_GeneratesToken()
    {
        var token = _sut.GenerateToken(new List<Claim>() { new ("test", "test")});
        Assert.Equal("bmwadforth.com", token.Issuer);
    }
    
    [Fact]
    public async void JwtAuthenticationServiceTest_HashesPassword()
    {
        var hash = _sut.HashPassword("my_password");
        Assert.NotNull(hash);
    }
    
    [Fact]
    public async void JwtAuthenticationServiceTest_ValidatesHashedPassword()
    {
        var validateHash = _sut.ValidateHash("my_password", "$2a$11$1Fy9dWPjk1Neb7l5XLgkNeY.ybt5jZO9Xw0wZ2EohWRmeg8gKnWh.");
        Assert.True(validateHash);
    }
    
    [Fact]
    public async void JwtAuthenticationServiceTest_ValidatesHashedPasswordWhenHashIsWrong()
    {
        var validateHash = _sut.ValidateHash("my_password", "$2a$11$1Fy9dWPjk1Neb7l5XLgkNeY.fsdfsdfsdfsdftestestestest.");
        Assert.False(validateHash);
    }
}