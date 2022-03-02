using Microsoft.EntityFrameworkCore;

namespace Bmwadforth.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) {}

    public DbSet<Blog> Blogs { get; set; }
}