namespace Bmwadforth.Models;

public interface IApiResponse<T>
{
    public string Message { get; set; }
    public T Payload { get; set; }
    public List<IApiError>? Errors { get; set; }
}