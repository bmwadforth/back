namespace BlogWebsite.Common.Interfaces;

public interface IApiError
{
    string Code { get; set; }
    string Error { get; set; }
}