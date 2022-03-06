using Bmwadforth.Handlers;
using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
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
    public async Task<IApiResponse<IEnumerable<Article>>> GetAll() => await _Mediator.Send(new GetArticlesRequest());
    
    [HttpGet("{articleId}")]
    public async Task<IApiResponse<Article>> Get([FromRoute] int articleId) => await _Mediator.Send(new GetArticleRequest(articleId));

    [HttpPost]
    public async Task<IApiResponse<int>> Create([FromBody] Article request) => await _Mediator.Send(new CreateArticleRequest(request));

    
    [HttpGet("content")]
    public async Task<IActionResult> GetContent([FromQuery] int articleId)
    {
        var response = await _Mediator.Send(new GetArticleContentRequest(articleId));
        return File(response.Item1, response.Item2);
    }
    
    [HttpPost("content")]
    public async Task<IApiResponse<int>> CreateContent([FromQuery] int articleId) => await _Mediator.Send(new CreateArticleContentRequest(articleId, Request.ContentType ?? "application/octet-stream", Request.Body));
    
    [HttpGet("thumbnail")]
    public async Task<IActionResult> GetThumbnail([FromQuery] int articleId)
    {
        var (stream, contentType) = await _Mediator.Send(new GetArticleThumbnailRequest(articleId));
        return File(stream, contentType);
    }
    
    [HttpPost("thumbnail")]
    public async Task<IApiResponse<int>> CreateThumbnail([FromQuery] int articleId) => await _Mediator.Send(new CreateArticleThumbnailRequest(articleId, Request.ContentType ?? "application/octet-stream", Request.Body));
}