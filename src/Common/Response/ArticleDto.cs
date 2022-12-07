namespace BlogWebsite.Common.Response;

public class ArticleDto
{
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? ThumbnailId { get; set; }
    public string? ThumbnailDataUrl { get; set; }
    public Guid? ContentId { get; set; }
    public string? ContentDataUrl { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
}