using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Response;
using MediatR;

namespace Bmwadforth.Handlers;

public sealed record LoginUserRequest(string Username, string Password) : IRequest<IApiResponse<string>>;

public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, IApiResponse<string>>
{
    private readonly IUserRepository _repository;

    public LoginUserRequestHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<string>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var token = await _repository.LoginUser(request.Username, request.Password);
        return new ApiResponse<string>("success", token, null);
    }
}