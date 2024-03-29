using BlogWebsite.Common.Configuration;
using BlogWebsite.Common.Exceptions;
using BlogWebsite.Common.Interfaces;
using Google;
using Google.Cloud.Storage.V1;
using System.Net;

namespace BlogWebsite.Common.Repositories;

public class BlobRepository : IBlobRepository
{
    private readonly BlobConfiguration _blobConfiguration;

    public BlobRepository(IConfiguration configuration)
    {
        _blobConfiguration = new BlobConfiguration();
        configuration.GetSection("Blob").Bind(_blobConfiguration);
    }

    public async Task<(Stream, string)> GetBlob(Guid id)
    {
        var storage = await StorageClient.CreateAsync();

        try
        {
            var storageObject = await storage.GetObjectAsync(_blobConfiguration.Bucket,
                $"{_blobConfiguration.Folder}/{id}", new GetObjectOptions { Projection = Projection.Full });
            var stream = new MemoryStream();
            await storage.DownloadObjectAsync(_blobConfiguration.Bucket, $"{_blobConfiguration.Folder}/{id}", stream);
            stream.Position = 0;
            return (stream, storageObject.ContentType);
        }
        catch (GoogleApiException e)
        {
            if (e.HttpStatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException($"blob with ID {id} not found");
            }

            throw;
        }
    }

    public async Task NewBlob(Guid id, string contentType, Stream source)
    {
        var storage = await StorageClient.CreateAsync();
        await storage.UploadObjectAsync(_blobConfiguration.Bucket, $"{_blobConfiguration.Folder}/{id}", contentType, source);
    }
}