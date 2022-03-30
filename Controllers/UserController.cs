using System.Net;
using Bmwadforth.Handlers;
using Bmwadforth.Common.Middleware;
using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Request;
using Bmwadforth.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bmwadforth.Controllers;

[ApiController]
[Route("/api/v1/user")]
public class UserController : ApiController<UserController>
{
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger) : base(mediator, logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IApiResponse<int>> Create([FromQuery] string username, [FromQuery] string password) => await _Mediator.Send(new CreateUserRequest(username, password));
}