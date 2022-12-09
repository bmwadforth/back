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
        var config = ConfigHelper.GetConfig();
        _sut = new JwtAuthenticationService(config);
    }
    
    [Fact]
    public async void JwtAuthenticationServiceTest_GeneratesToken()
    {
        var token = _sut.GenerateToken(new List<Claim>() { new ("test", "test")});
        Assert.Equal("bmwadforth.com", token.Issuer);
    }
}