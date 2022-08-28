using System.Net;
using Bmwadforth.Common.Exceptions;
using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bmwadforth.Common.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment hostEnvironment)
    {
        _next = next;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Invoke(HttpContext context)
    {
        HttpStatusCode statusCode;
        var errors = new List<IApiError>();
        var response = new ApiResponse<object?>("An error has occurred", null, errors);

        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError($"Exception has occurred: {e.Message}");

            if (_hostEnvironment.IsDevelopment()) throw;

            switch (e)
            {
                case ValidationException validationException:
                {
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                }
                case UserAuthenticationException userAuthenticationException:
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                }
                case NotFoundException notFoundException:
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

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}