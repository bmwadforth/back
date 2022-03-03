using System.Net;
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
            return;
        }
        
        _logger.LogError($"Exeption has occurred: {context.Exception.Message}");

        var errors = new List<IApiError>();
        var response = new ApiResponse<object>(context.Exception.Message, null, errors);

        context.Result = new ObjectResult(response)
        {
            StatusCode = (int) HttpStatusCode.InternalServerError
        };
    }
}