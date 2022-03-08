using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
using Bmwadforth.Types.Response;
using MediatR;

namespace Bmwadforth.Handlers;

public sealed record GetArticlesRequest() : IRequest<IApiResponse<IEnumerable<ArticleDto>>>;

public class GetArticlesRequestHandler : IRequestHandler<GetArticlesRequest, IApiResponse<IEnumerable<ArticleDto>>>
{
    private readonly IArticleRepository _repository;

    public GetArticlesRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<IEnumerable<ArticleDto>>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {
        var articles = await _repository.GetArticles();
        return new ApiResponse<IEnumerable<ArticleDto>>("success", articles, null);
    }
}