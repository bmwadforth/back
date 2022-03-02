using Bmwadforth.Models;

namespace Bmwadforth.Services;

public interface IArticleService
{
    IApiResponse<List<Article>> GetArticles();
    Task<IApiResponse<int>> NewArticle(Article article);
}