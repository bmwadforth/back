using Bmwadforth.Handlers;
using Bmwadforth.Middleware;
using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
using Bmwadforth.Types.Request;
using Bmwadforth.Types.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IApiResponse<IEnumerable<ArticleDto>>> GetAll() {
        var res = await _Mediator.Send(new GetArticlesRequest());
        Response.StatusCode = (int) res.StatusCode;
        return res;
    }
    
    [HttpGet("{articleId}")]
    public async Task<IApiResponse<ArticleDto>> Get([FromRoute] int articleId) {
        var res = await _Mediator.Send(new GetArticleRequest(articleId));
        Response.StatusCode = (int) res.StatusCode;
        return res;
    }

    [ApiKey]
    [HttpPost]
    public async Task<IApiResponse<int>> Create([FromBody] CreateArticleDto request)
    {
        var res = await _Mediator.Send(new CreateArticleRequest(request));
        Response.StatusCode = (int) res.StatusCode;
        return res;
    }

    
    [HttpGet("content/{articleId}")]
    public async Task<IActionResult> GetContent([FromRoute] int articleId)
    {
        var (stream, contentType) = await _Mediator.Send(new GetArticleContentRequest(articleId));
        return File(stream, contentType);
    }

    [ApiKey]
    [HttpPost("content/{articleId}")]
    public async Task<IApiResponse<int>> CreateContent([FromRoute] int articleId)
    {
        var res = await _Mediator.Send(new CreateArticleContentRequest(articleId, Request.ContentType ?? "application/octet-stream", Request.Body));
        Response.StatusCode = (int) res.StatusCode;
        return res;
    }
    
    [HttpGet("thumbnail/{articleId}")]
    public async Task<IActionResult> GetThumbnail([FromRoute] int articleId)
    {
        var (stream, contentType) = await _Mediator.Send(new GetArticleThumbnailRequest(articleId));
        return File(stream, contentType);
    }

    [ApiKey]
    [HttpPost("thumbnail/{articleId}")]
    public async Task<IApiResponse<int>> CreateThumbnail([FromRoute] int articleId)
    {
        var res = await _Mediator.Send(new CreateArticleThumbnailRequest(articleId, Request.ContentType ?? "application/octet-stream", Request.Body));
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }
}