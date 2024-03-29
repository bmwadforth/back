using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using BlogWebsite.Common.Models.Common;
using Newtonsoft.Json;

namespace BlogWebsite.Common.Handlers.User;

public sealed record LoginUserRequest(string Username, string Password) : IRequest<IApiResponse<string>>;

public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, IApiResponse<string>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public LoginUserRequestHandler(IUserRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<IApiResponse<string>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var (user, token) = await _repository.LoginUser(request.Username, request.Password);

        var userData = new UserData
        {
            UserName = user.Username,
            Token = token,
            LoggedInSince = DateTimeOffset.Now,
        };

        var userDataString = JsonConvert.SerializeObject(userData);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.UserData, userDataString),
        }, CookieAuthenticationDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await _httpContext.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        return new ApiResponse<string>("success", token, null);
    }
}