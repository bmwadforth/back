using BlogWebsite.Common.Models.Common;
using BlogWebsite.Common.Handlers.User;
using BlogWebsite.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers;

[ApiController]
[Route("/api/v1/user")]
public class UserController : ApiController<UserController>
{
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger) : base(mediator, logger)
    {
        _logger = logger;
    }

    [Authorize]
    [HttpPost]
    public async Task<IApiResponse<int>> Create([FromQuery] string username, [FromQuery] string password) => await _Mediator.Send(new CreateUserRequest(username, password));

    [HttpPost("login")]
    public async Task<IApiResponse<string>> Login([FromQuery] string username, [FromQuery] string password) => await _Mediator.Send(new LoginUserRequest(username, password));

    [Authorize]
    [HttpGet("status")]
    public async Task<IApiResponse<UserData>> Status() => await _Mediator.Send(new UserStatusRequest());
}