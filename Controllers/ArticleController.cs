using Bmwadforth.Models;
using Bmwadforth.Services;
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
}