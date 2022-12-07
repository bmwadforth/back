using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Response;
using MediatR;

namespace BlogWebsite.Common.Handlers.Articles;

public sealed record CreateArticleThumbnailRequest(int ArticleId, string ContentType, Stream Source) : IRequest<IApiResponse<int>>;

public class CreateArticleThumbnailRequestHandler : IRequestHandler<CreateArticleThumbnailRequest, IApiResponse<int>>
{
    private readonly IArticleRepository _repository;

    public CreateArticleThumbnailRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<int>> Handle(CreateArticleThumbnailRequest request, CancellationToken cancellationToken)
    {
        await _repository.NewArticleThumbnail(request.ArticleId, request.ContentType, request.Source);
        return new ApiResponse<int>("success", request.ArticleId, null);
    }
}