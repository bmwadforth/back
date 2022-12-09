using BlogWebsite.Common.Models;
using BlogWebsite.Common.Request;
using BlogWebsite.Common.Response;

namespace BlogWebsite.Common.Interfaces;

public interface IArticleRepository
{
    Task<Article> GetArticle(int id, bool includeUnpublished = false);
    Task<ArticleDto?> GetArticleById(int id);
    Task<List<ArticleDto>> GetArticles();
    Task<int> NewArticle(CreateArticleDto article);
    Task PublishArticle(int id);
    Task UpdateArticle(Article article);
    Task<(Stream, string)> GetArticleContent(int id);
    Task NewArticleContent(int id, string contentType, Stream source);
    Task<(Stream, string)> GetArticleThumbnail(int id);
    Task NewArticleThumbnail(int id, string contentType, Stream source);
}