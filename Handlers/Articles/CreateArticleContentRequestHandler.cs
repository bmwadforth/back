using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Models;
using Bmwadforth.Common.Response;
using MediatR;

namespace Bmwadforth.Handlers;

public sealed record CreateArticleContentRequest(int ArticleId, string ContentType, Stream Source) : IRequest<IApiResponse<int>>;

public class CreateArticleContentRequestHandler : IRequestHandler<CreateArticleContentRequest, IApiResponse<int>>
{
    private readonly IArticleRepository _repository;

    public CreateArticleContentRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IApiResponse<int>> Handle(CreateArticleContentRequest request, CancellationToken cancellationToken)
    {
        await _repository.NewArticleContent(request.ArticleId, request.ContentType, request.Source);
        return new ApiResponse<int>("success", request.ArticleId, null);
    }
}