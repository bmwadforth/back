using Bmwadforth.Repositories;
using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bmwadforth.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly ILogger<ArticleController> _logger;
    private readonly IArticleService _articleService;

    public ArticleController(ILogger<ArticleController> logger, IArticleService articleService)
    {
        _logger = logger;
        _articleService = articleService;
    }

    [HttpGet]
    public IApiResponse<List<Article>> Get() => _articleService.GetArticles();
    
    [HttpPost]
    public async Task<IApiResponse<int>> Create([FromBody] Article article) => await _articleService.NewArticle(article);

    [HttpGet("content")]
    public IActionResult GetContent([FromQuery] int articleId)
    {
        var article = _articleService.GetArticle(articleId);
        var (content, contentType) = _articleService.GetArticleContent(article.Content.Value);
        return File(content, contentType);
    }
    
    [HttpPut("content")]
    public IApiResponse<Guid> CreateContent([FromQuery] int articleId) => _articleService.NewArticleContent(articleId, Request.ContentType ?? "application/octet-stream", Request.Body);
}