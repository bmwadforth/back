using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using BlogWebsite.Common.Models.Common;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BlogWebsite.Common.Handlers.User;

public sealed record UserStatusRequest() : IRequest<IApiResponse<UserData>>;

public class UserStatusRequestHandler : IRequestHandler<UserStatusRequest, IApiResponse<UserData>>
{
    private readonly IUserRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public UserStatusRequestHandler(IUserRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<IApiResponse<UserData>> Handle(UserStatusRequest request, CancellationToken cancellationToken)
    {
        var userData = _httpContext.HttpContext.Items["UserData"];        
        
        return new ApiResponse<UserData>("success", (UserData) userData, null);
    }
}