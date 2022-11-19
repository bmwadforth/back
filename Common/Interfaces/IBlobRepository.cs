namespace Bmwadforth.Common.Interfaces;

public interface IBlobRepository
{
    Task<(Stream, string)> GetBlob(Guid id);
    Task NewBlob(Guid id, string contentType, Stream source);
}