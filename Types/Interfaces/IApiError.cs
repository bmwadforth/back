namespace Bmwadforth.Types.Interfaces;

public interface IApiError
{
    string Code { get; set; }
    string Error { get; set; }
}