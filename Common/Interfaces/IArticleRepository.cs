using Bmwadforth.Common.Models;
using Bmwadforth.Common.Request;
using Bmwadforth.Common.Response;

namespace Bmwadforth.Common.Interfaces;

public interface IArticleRepository
{
    Task<Article> GetArticle(int id);
    Task<ArticleDto?> GetArticleById(int id);
    Task<List<ArticleDto>> GetArticles();
    Task<int> NewArticle(CreateArticleDto article);
    Task UpdateArticle(Article article);
    Task<(Stream, string)> GetArticleContent(int id);
    Task NewArticleContent(int id, string contentType, Stream source);
    Task<(Stream, string)> GetArticleThumbnail(int id);
    Task NewArticleThumbnail(int id, string contentType, Stream source);
}