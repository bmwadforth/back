using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogWebsite.Common.Interfaces;

public interface IJwtAuthenticationService
{
    JwtSecurityToken GenerateToken(IEnumerable<Claim> authClaims);
    bool ValidateToken(string token);
    string HashPassword(string password);
    bool ValidateHash(string password, string hash);
}