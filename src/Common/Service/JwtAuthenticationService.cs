using BlogWebsite.Common.Configuration;
using BlogWebsite.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogWebsite.Common.Service;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly AuthenticationConfiguration _authenticationConfiguration;

    public JwtAuthenticationService(IConfiguration configuration)
    {
        _authenticationConfiguration = configuration.GetSection("Authentication").Get<AuthenticationConfiguration>();
    }

    public JwtSecurityToken GenerateToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfiguration.Key));

        var token = new JwtSecurityToken(
            issuer: _authenticationConfiguration.Issuer,
            audience: _authenticationConfiguration.Audience,
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationConfiguration.Key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out _);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool ValidateHash(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}