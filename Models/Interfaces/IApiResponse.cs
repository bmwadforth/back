namespace Bmwadforth.Models;

public interface IApiResponse<T>
{
    string Message { get; set; }
    T Payload { get; set; }
    List<IApiError>? Errors { get; set; }
}