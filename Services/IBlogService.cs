using Bmwadforth.Models;

namespace Bmwadforth.Services;

public interface IBlogService
{
    List<Blog> GetBlogs();
}