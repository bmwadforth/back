using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogWebsite.Common.Interfaces;

public interface IJwtAuthenticationService
{
    JwtSecurityToken GenerateToken(List<Claim> authClaims);
    string HashPassword(string password);
    bool ValidateHash(string password, string hash);
}