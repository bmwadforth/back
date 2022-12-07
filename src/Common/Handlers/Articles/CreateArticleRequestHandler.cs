using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Request;
using BlogWebsite.Common.Response;
using MediatR;

namespace BlogWebsite.Common.Handlers.Articles;

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