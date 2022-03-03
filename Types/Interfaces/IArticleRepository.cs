using Bmwadforth.Types.Interfaces;
using Bmwadforth.Types.Models;

namespace Bmwadforth.Types.Interfaces;

public interface IArticleRepository
{
    Task<Article> GetArticle(int id);
    Task<List<Article>> GetArticles();
    Task<(Stream, string)> GetArticleContent(int id);
    Task<int> NewArticle(Article article);
    Task NewArticleContent(int id, string contentType, Stream source);
    Task UpdateArticle(Article article);
}