using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;

namespace Bmwadforth.Repositories;

public interface IArticleService
{
    Article GetArticle(int id);
    IApiResponse<List<Article>> GetArticles();
    (Stream, string) GetArticleContent(Guid id);
    Task<IApiResponse<int>> NewArticle(Article article);
    IApiResponse<Guid> NewArticleContent(int id, string contentType, Stream source);
    void UpdateArticle(Article article);
}