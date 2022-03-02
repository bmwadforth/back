using Bmwadforth.Models;

namespace Bmwadforth.Services;

public class ArticleService : IArticleService
{
    private readonly Context _context;
    
    public ArticleService(Context context)
    {
        _context = context;
    }
    
    public IApiResponse<List<Article>> GetArticles()
    {
        var articles = _context.Articles.Select(b => b).ToList();
        return new ApiResponse<List<Article>>("Articles fetched successfully", articles, null);
    }

    public async Task<IApiResponse<int>> NewArticle(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return new ApiResponse<int>("Article created successfully", article.ArticleId, null);
    }
}