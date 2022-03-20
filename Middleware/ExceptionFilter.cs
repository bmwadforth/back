using System.Net;
using Bmwadforth.Types.Exceptions;
using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bmwadforth.Middleware;

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
        if (_hostEnvironment.IsDevelopment())
        {
            //return;
        }
        
        _logger.LogError($"Exception has occurred: {context.Exception.Message}");

        HttpStatusCode statusCode;
        var errors = new List<IApiError>();
        ApiResponse<object> response = new ApiResponse<object>(context.Exception.Message, null, errors);

        switch (context.Exception)
        {
            case ValidationException:
            {
                statusCode = HttpStatusCode.BadRequest;
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