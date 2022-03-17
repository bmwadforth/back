using System.Net;

namespace Bmwadforth.Types.Interfaces;

public interface IApiResponse<T>
{
    HttpStatusCode StatusCode { get; set; }
    string Message { get; set; }
    T Payload { get; set; }
    List<IApiError>? Errors { get; set; }
}