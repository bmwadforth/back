using Bmwadforth.Models;

namespace Bmwadforth.Services;

public interface IArticleService
{
    IApiResponse<List<Article>> GetArticles();
    (Stream, string) GetArticleContent(Guid id);
    Task<IApiResponse<int>> NewArticle(Article article);
}