using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bmwadforth.Controllers;

[Route("/[controller]/[action]")]
[ApiController]
public abstract class ApiController<T> : ControllerBase
{ 
    protected IMediator _Mediator { get; }
    protected ILogger<T> _Logger { get; }

    protected ApiController(IMediator mediator, ILogger<T> logger)
    {
        _Mediator = mediator;
        _Logger = logger;
    }
}