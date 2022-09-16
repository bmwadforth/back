namespace Bmwadforth.Common.Models;

public class Project : BaseEntity
{
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}