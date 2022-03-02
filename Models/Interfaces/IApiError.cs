namespace Bmwadforth.Models;

public interface IApiError
{
    string Code { get; set; }
    string Error { get; set; }
}