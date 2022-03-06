namespace Bmwadforth.Types.Request;

public class CreateArticleDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? Thumbnail { get; set; }
    public Guid? Content { get; set; }
}