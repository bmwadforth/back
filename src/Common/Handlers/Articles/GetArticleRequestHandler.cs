using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Response;
using MediatR;

namespace BlogWebsite.Common.Handlers.Articles;

public sealed record GetArticleRequest(int ArticleId) : IRequest<IApiResponse<ArticleDto>>;

public class GetArticleRequestHandler : IRequestHandler<GetArticleRequest, IApiResponse<ArticleDto>>
{
    private readonly IArticleRepository _repository;

    public GetArticleRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<ArticleDto>> Handle(GetArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await _repository.GetArticleById(request.ArticleId);
        return article == null ? new ApiResponse<ArticleDto>("article not found", null!, null) : new ApiResponse<ArticleDto>("success", article, null);
    }
}