namespace Bmwadforth.Repositories;

public interface IBlobService
{
    Task<(Stream, string)> GetBlob(Guid id);
    void NewBlob(Guid id, string contentType, Stream source);
}