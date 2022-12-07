using BlogWebsite.Common.Interfaces;
using MediatR;

namespace BlogWebsite.Common.Handlers.Articles;

public sealed record GetArticleThumbnailRequest(int ArticleId) : IRequest<(Stream, string)>;

public class GetArticleThumbnailRequestHandler : IRequestHandler<GetArticleThumbnailRequest, (Stream, string)>
{
    private readonly IArticleRepository _repository;

    public GetArticleThumbnailRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<(Stream, string)> Handle(GetArticleThumbnailRequest request, CancellationToken cancellationToken) => await _repository.GetArticleThumbnail(request.ArticleId);
}