using System.Net;
using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bmwadforth.Common.Middleware;

public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string APIKEYNAME = "ApiKey";
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            var errors = new List<IApiError>();
            var response = new ApiResponse<object>("authentication failure", null, errors);
            context.Result = new ObjectResult(response)
            {
                StatusCode = (int) HttpStatusCode.Unauthorized
            };
            return;
        }
 
        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
 
        var apiKey = appSettings.GetValue<string>(APIKEYNAME);
 
        if (!apiKey.Equals(extractedApiKey))
        {
            var errors = new List<IApiError>();
            var response = new ApiResponse<object>("authentication failure", null, errors);
            context.Result = new ObjectResult(response)
            {
                StatusCode = (int) HttpStatusCode.Unauthorized
            };
            return;
        }
 
        await next();
    }
}