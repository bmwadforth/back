using System.Security.Claims;
using System.Text.Encodings.Web;
using BlogWebsite.Common.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace BlogWebsite.Common.AuthenticationSchemes;

public static class ApiKeyAuthenticationDefaults
{
    public const string AuthenticationScheme = "ApiKey";
}
public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions{};

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private const string AuthorizationHeaderName = "X-Api-Key";
    private readonly string _apiKey;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IConfiguration configuration)
        : base(options, logger, encoder, clock)
    {
        _apiKey = configuration.GetValue<string>(ApiKeyAuthenticationDefaults.AuthenticationScheme);
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(AuthorizationHeaderName))
        {
            throw new AuthenticationException("Request does not contain API Key");
        }

        if (!Request.Headers.TryGetValue(AuthorizationHeaderName, out var headerValue))
        {
            throw new AuthenticationException("Invalid authorization header");   
        }

        if (string.IsNullOrEmpty(headerValue))
        {
            throw new AuthenticationException("Missing API key");
        }

        if (!_apiKey.Equals(headerValue))
        {
            throw new AuthenticationException("Authentication failed");
        }
        
        var claims = new[] { new Claim(ClaimTypes.Name, "Service") };            
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}