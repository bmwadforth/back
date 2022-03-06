using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
using Bmwadforth.Types.Request;
using Bmwadforth.Types.Response;
using MediatR;

namespace Bmwadforth.Handlers;

public sealed record CreateArticleRequest(CreateArticleDto Article) : IRequest<IApiResponse<int>>;

public class CreateArticleRequestHandler : IRequestHandler<CreateArticleRequest, IApiResponse<int>>
{
    private readonly IArticleRepository _repository;

    public CreateArticleRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<int>> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        var articleId = await _repository.NewArticle(request.Article);
        return new ApiResponse<int>("success", articleId, null);
    }
}