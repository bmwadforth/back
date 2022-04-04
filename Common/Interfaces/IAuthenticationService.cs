
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bmwadforth.Common.Interfaces;

public interface IAuthenticationService
{
    JwtSecurityToken GenerateToken(List<Claim> authClaims);
    string HashPassword(string password);
    bool ValidateHash(string password, string hash);
}