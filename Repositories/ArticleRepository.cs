using Bmwadforth.Common.Configuration;
using Bmwadforth.Common.Exceptions;
using Bmwadforth.Common.Interfaces;
using Bmwadforth.Common.Models;
using Bmwadforth.Common.Request;
using Bmwadforth.Common.Response;
using Microsoft.EntityFrameworkCore;

namespace Bmwadforth.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly IBlobRepository _blobRepository;
    private readonly ConnectionStringConfiguration _configuration;
    
    public ArticleRepository(DatabaseContext databaseContext, IBlobRepository blobRepository, IConfiguration configuration)
    {
        _databaseContext = databaseContext;
        _blobRepository = blobRepository;
        _configuration = new ConnectionStringConfiguration();
        configuration.GetSection("ConnectionStrings").Bind(_configuration);
    }

    public async Task<Article> GetArticle(int id) => await _databaseContext.Articles.FirstOrDefaultAsync(a => a.ArticleId == id && a.Published) ?? throw new NotFoundException($"article with {id} not found");
    public async Task<ArticleDto?> GetArticleById(int id)
    {
        var article = await _databaseContext.Articles.FirstOrDefaultAsync(a => a.ArticleId == id && a.Published);
        if (article == null) throw new NotFoundException($"article with {id} not found");
        
        return new ArticleDto
        {
            ArticleId = article.ArticleId,
            Title = article.Title,
            Description = article.Description,
            ContentId = article.ContentId,
            ThumbnailId = article.ThumbnailId,
            ThumbnailDataUrl = $"{_configuration.ContentDeliveryNetwork}/{article.ThumbnailId}",
            ContentDataUrl = $"{_configuration.ContentDeliveryNetwork}/{article.ContentId}",
            CreatedDate = article.CreatedDate,
            UpdatedDate = article.UpdatedDate
        };
    }

    public async Task<List<ArticleDto>> GetArticles()
    {
        var articles = await _databaseContext.Articles.ToListAsync();

        return articles.Where(a => a.Published).Select(article => new ArticleDto
            {
                ArticleId = article.ArticleId,
                Title = article.Title,
                Description = article.Description,
                ContentId = article.ContentId,
                ThumbnailId = article.ThumbnailId,
                ThumbnailDataUrl = article.ThumbnailId == null ? null : $"{_configuration.ContentDeliveryNetwork}/{article.ThumbnailId}",
                ContentDataUrl = article.ContentId == null ? null : $"{_configuration.ContentDeliveryNetwork}/{article.ContentId}",
                CreatedDate = article.CreatedDate,
                UpdatedDate = article.UpdatedDate
            })
            .ToList();
    }

    public async Task<(Stream, string)> GetArticleContent(int id)
    {
        var article = await GetArticle(id);
        if (article.ContentId == null) throw new NotFoundException($"article with {id} does not have content");
        return await _blobRepository.GetBlob(article.ContentId.Value);
    }
    
    public async Task NewArticleContent(int articleId, string contentType, Stream source)
    {
        var article = await GetArticle(articleId);
        var id = article.ContentId ?? Guid.NewGuid();
        article.ContentId = id;
        await _blobRepository.NewBlob(id, contentType, source);
        await UpdateArticle(article);
    }

    public async Task<(Stream, string)> GetArticleThumbnail(int id)
    {
        var article = await GetArticle(id);
        if (article.ThumbnailId == null) throw new NotFoundException($"article with {id} does not have a thumbnail");
        return await _blobRepository.GetBlob(article.ThumbnailId.Value);
    }

    public async Task NewArticleThumbnail(int articleId, string contentType, Stream source)
    {
        var article = await GetArticle(articleId);
        var id = article.ThumbnailId ?? Guid.NewGuid();
        article.ThumbnailId = id;
        await _blobRepository.NewBlob(id, contentType, source);
        await UpdateArticle(article);
    }

    public async Task UpdateArticle(Article article)
    {
        _databaseContext.Update(article);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<int> NewArticle(CreateArticleDto article)
    {
        var newArticle = new Article
        {
            Title = article.Title,
            Description = article.Description,
            ContentId = article.Content,
            ThumbnailId = article.Thumbnail,
            Published = false
        };

        _databaseContext.Articles.Add(newArticle);
        await _databaseContext.SaveChangesAsync();
        return newArticle.ArticleId;
    }
}