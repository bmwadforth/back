using System.Net;
using Bmwadforth.Types.Interfaces;

namespace Bmwadforth.Types.Response;

public class ApiResponse<T> : IApiResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public T Payload { get; set; }
    public List<IApiError>? Errors { get; set; }
    
    public ApiResponse(string message, T payload, List<IApiError>? errors, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        StatusCode = statusCode;
        Message = message;
        Payload = payload;
        Errors = errors;
    }
}