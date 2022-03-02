using Bmwadforth.Models;
using Bmwadforth.Models.Configuration;
using Google.Cloud.Storage.V1;

namespace Bmwadforth.Services;

public class BlobService : IBlobService
{
    private readonly BlobConfiguration _blobConfiguration;
    
    public BlobService(IConfiguration configuration)
    {
        _blobConfiguration = new BlobConfiguration();
        configuration.GetSection("Blob").Bind(_blobConfiguration);
    }

    public async Task<(Stream, string)> GetBlob(Guid id)
    {
        var storage = await StorageClient.CreateAsync();

        var storageObject = await storage.GetObjectAsync(_blobConfiguration.Bucket, $"{_blobConfiguration.Folder}/{id}", new GetObjectOptions { Projection = Projection.Full });
        var stream = new MemoryStream();
        await storage.DownloadObjectAsync(_blobConfiguration.Bucket, $"{_blobConfiguration.Folder}/{id}", stream);
        stream.Position = 0;
        
        return (stream, storageObject.ContentType);
    }

    public void NewBlob(Guid id, string contentType, Stream source)
    {
        var storage = StorageClient.Create();

        storage.UploadObject(_blobConfiguration.Bucket, $"{_blobConfiguration.Folder}/{id}", contentType, source);
    }
}