using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Models;
using Bmwadforth.Common.Response;
using MediatR;

namespace Bmwadforth.Handlers;

public sealed record GetArticleContentRequest(int ArticleId) : IRequest<(Stream, string)>;

public class GetArticleContentRequestHandler : IRequestHandler<GetArticleContentRequest, (Stream, string)>
{
    private readonly IArticleRepository _repository;

    public GetArticleContentRequestHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<(Stream, string)> Handle(GetArticleContentRequest request, CancellationToken cancellationToken) => await _repository.GetArticleContent(request.ArticleId);
}