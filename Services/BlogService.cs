using Bmwadforth.Models;

namespace Bmwadforth.Services;

public class BlogService : IBlogService
{
    private readonly Context _context;
    
    public BlogService(Context context)
    {
        _context = context;
    }
    
    public List<Blog> GetBlogs()
    {
        var blogs = _context.Blogs.Select(b => b).ToList();

        return blogs;
    }
}