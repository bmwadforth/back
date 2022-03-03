using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
using Bmwadforth.Types.Response;
using MediatR;

namespace Bmwadforth.Handlers;

public sealed record GetArticleRequest(int ArticleId) : IRequest<IApiResponse<Article>>;

public class GetArticleRequestHandler : IRequestHandler<GetArticleRequest, IApiResponse<Article>>
{
    private readonly IArticleRepository _repository;

    public GetArticleRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<Article>> Handle(GetArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await _repository.GetArticle(request.ArticleId);
        throw new Exception("Error");
        return new ApiResponse<Article>("success", article, null);
    }
}