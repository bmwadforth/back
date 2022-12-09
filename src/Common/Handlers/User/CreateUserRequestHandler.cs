using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Response;
using MediatR;

namespace BlogWebsite.Common.Handlers.User;

public sealed record CreateUserRequest(string Username, string Password) : IRequest<IApiResponse<int>>;

public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, IApiResponse<int>>
{
    private readonly IUserRepository _repository;

    public CreateUserRequestHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<int>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userId = await _repository.CreateUser(request.Username, request.Password);
        return new ApiResponse<int>("success", userId, null);
    }
}