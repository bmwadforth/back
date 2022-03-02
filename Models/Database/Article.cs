namespace Bmwadforth.Models;

public class Article : BaseEntity
{
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid Thumbnail { get; set; }
    public Guid Content { get; set; }
}