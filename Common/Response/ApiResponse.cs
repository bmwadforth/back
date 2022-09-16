using System.Net;
using Bmwadforth.Common.Interfaces;

namespace Bmwadforth.Common.Response;

public class ApiResponse<T> : IApiResponse<T>
{
    public string Message { get; set; }
    public T Payload { get; set; }
    public List<IApiError>? Errors { get; set; }
    
    public ApiResponse(string message, T payload, List<IApiError>? errors, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Message = message;
        Payload = payload;
        Errors = errors;
    }
}