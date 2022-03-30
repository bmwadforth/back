using System.Net;
using Bmwadforth.Handlers;
using Bmwadforth.Common.Middleware;
using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Request;
using Bmwadforth.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bmwadforth.Controllers;

[ApiController]
[Route("/api/v1/article")]
public class ArticleController : ApiController<ArticleController>
{
    private readonly ILogger<ArticleController> _logger;

    public ArticleController(IMediator mediator, ILogger<ArticleController> logger) : base(mediator, logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IApiResponse<IEnumerable<ArticleDto>>> GetAll() => await _Mediator.Send(new GetArticlesRequest());
    
    [HttpGet("{articleId}")]
    public async Task<IApiResponse<ArticleDto>> Get([FromRoute] int articleId) => await _Mediator.Send(new GetArticleRequest(articleId));

    [ApiKey]
    [HttpPost]
    public async Task<IApiResponse<int>> Create([FromBody] CreateArticleDto request) => await _Mediator.Send(new CreateArticleRequest(request));

    
    [HttpGet("content/{articleId}")]
    public async Task<IActionResult> GetContent([FromRoute] int articleId)
    {
        var (stream, contentType) = await _Mediator.Send(new GetArticleContentRequest(articleId));
        return File(stream, contentType);
    }

    [ApiKey]
    [HttpPost("content/{articleId}")]
    public async Task<IApiResponse<int>> CreateContent([FromRoute] int articleId) => await _Mediator.Send(new CreateArticleContentRequest(articleId, Request.ContentType ?? "application/octet-stream", Request.Body));
    
    [HttpGet("thumbnail/{articleId}")]
    public async Task<IActionResult> GetThumbnail([FromRoute] int articleId)
    {
        var (stream, contentType) = await _Mediator.Send(new GetArticleThumbnailRequest(articleId));
        return File(stream, contentType);
    }

    [ApiKey]
    [HttpPost("thumbnail/{articleId}")]
    public async Task<IApiResponse<int>> CreateThumbnail([FromRoute] int articleId) => await _Mediator.Send(new CreateArticleThumbnailRequest(articleId, Request.ContentType ?? "application/octet-stream", Request.Body));
}