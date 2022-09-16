using Bmwadforth.Common.Interfaces;

namespace Bmwadforth.Common.Response;

internal class ApiError : IApiError {
    public ApiError(string code, string error)
    {
        Code = code;
        Error = error;
    }

    public string Code { get; set; }
    public string Error { get; set; }
}