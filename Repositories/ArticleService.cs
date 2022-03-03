using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;
using Bmwadforth.Types.Response;

namespace Bmwadforth.Repositories;

public class ArticleService : IArticleService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IBlobService _blobService;
    private readonly IConfiguration _configuration;
    
    public ArticleService(DatabaseContext databaseContext, IBlobService blobService, IConfiguration configuration)
    {
        _databaseContext = databaseContext;
        _blobService = blobService;
        _configuration = configuration;
    }

    public Article GetArticle(int id)
    {
        return _databaseContext.Articles.FirstOrDefault(a => a.ArticleId == id);
    }

    public IApiResponse<List<Article>> GetArticles()
    {
        var articles = _databaseContext.Articles.ToList();
        return new ApiResponse<List<Article>>("Articles fetched successfully", articles, null);
    }

    public (Stream, string) GetArticleContent(Guid id)
    {
        return _blobService.GetBlob(id).Result;
    }
    
    public IApiResponse<Guid> NewArticleContent(int articleId, string contentType, Stream source)
    {
        var article = GetArticle(articleId);
        var id = article.Content ?? Guid.NewGuid();
        article.Content = id;
        _blobService.NewBlob(id, contentType, source);
        UpdateArticle(article);
        return new ApiResponse<Guid>("New article content created", id, null);
    }

    public void UpdateArticle(Article article)
    {
        _databaseContext.Update(article);
        _databaseContext.SaveChanges();
    }

    public async Task<IApiResponse<int>> NewArticle(Article article)
    {
        _databaseContext.Articles.Add(article);
        await _databaseContext.SaveChangesAsync();

        return new ApiResponse<int>("Article created successfully", article.ArticleId, null);
    }
}