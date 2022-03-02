namespace Bmwadforth.Models;

public interface IApiError
{
    public string Code { get; set; }
    public string Error { get; set; }
}