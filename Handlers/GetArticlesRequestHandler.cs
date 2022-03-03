using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
using Bmwadforth.Types.Response;
using MediatR;

namespace Bmwadforth.Handlers;

public sealed record GetArticlesRequest() : IRequest<IApiResponse<IEnumerable<Article>>>;

public class GetArticlesRequestHandler : IRequestHandler<GetArticlesRequest, IApiResponse<IEnumerable<Article>>>
{
    private readonly IArticleRepository _repository;

    public GetArticlesRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<IEnumerable<Article>>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {
        var articles = await _repository.GetArticles();
        return new ApiResponse<IEnumerable<Article>>("success", articles, null);
    }
}