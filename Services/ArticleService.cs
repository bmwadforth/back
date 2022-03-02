using Bmwadforth.Models;
using Google.Cloud.Storage.V1;

namespace Bmwadforth.Services;

public class ArticleService : IArticleService
{
    private readonly Context _context;
    private readonly IConfiguration _configuration;
    
    public ArticleService(Context context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public IApiResponse<List<Article>> GetArticles()
    {
        var articles = _context.Articles.Select(b => b).ToList();
        return new ApiResponse<List<Article>>("Articles fetched successfully", articles, null);
    }

    public (Stream, string) GetArticleContent(Guid id)
    {
        var blobConfig = _configuration.GetSection("Blob");
        var blobBucket = blobConfig.GetValue<string>("bucket");
        var blobFolder = blobConfig.GetValue<string>("folder");

        var storage = StorageClient.Create();

        var storageObject = storage.GetObject(blobBucket, $"{blobFolder}/{id}", new GetObjectOptions { Projection = Projection.Full });
        var stream = new MemoryStream();
        storage.DownloadObject(blobBucket, $"{blobFolder}/{id}", stream);
        stream.Position = 0;
        
        return (stream, storageObject.ContentType);
    }

    public async Task<IApiResponse<int>> NewArticle(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return new ApiResponse<int>("Article created successfully", article.ArticleId, null);
    }
}