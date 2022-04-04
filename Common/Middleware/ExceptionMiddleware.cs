using System.Net;
using Bmwadforth.Common.Exceptions;
using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bmwadforth.Common.Middleware;

public class ExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(IHostEnvironment hostEnvironment, ILogger<ExceptionFilter> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError($"Exception has occurred: {context.Exception.Message}");
        if (_hostEnvironment.IsDevelopment()) return;

        HttpStatusCode statusCode;
        var errors = new List<IApiError>();
        var response = new ApiResponse<object?>("An error has occurred", null, errors);

        switch (context.Exception)
        {
            case ValidationException:
            {
                statusCode = HttpStatusCode.BadRequest;
                break;
            }
            case UserAuthenticationException:
            {
                statusCode = HttpStatusCode.Unauthorized;
                break;
            }
            case NotFoundException:
            {
                statusCode = HttpStatusCode.NotFound;
                break;
            }
            default:
            {
                statusCode = HttpStatusCode.InternalServerError;
                break;
            }
        }

        context.Result = new ObjectResult(response)
        {
            StatusCode = (int) statusCode
        };
    }
}