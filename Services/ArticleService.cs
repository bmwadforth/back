using Bmwadforth.Models;
using Google.Cloud.Storage.V1;

namespace Bmwadforth.Services;

public class ArticleService : IArticleService
{
    private readonly Context _context;
    private readonly IBlobService _blobService;
    private readonly IConfiguration _configuration;
    
    public ArticleService(Context context, IBlobService blobService, IConfiguration configuration)
    {
        _context = context;
        _blobService = blobService;
        _configuration = configuration;
    }

    public Article GetArticle(int id)
    {
        return _context.Articles.FirstOrDefault(a => a.ArticleId == id);
    }

    public IApiResponse<List<Article>> GetArticles()
    {
        var articles = _context.Articles.ToList();
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
        _context.Update(article);
        _context.SaveChanges();
    }

    public async Task<IApiResponse<int>> NewArticle(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return new ApiResponse<int>("Article created successfully", article.ArticleId, null);
    }
}