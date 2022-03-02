using Microsoft.EntityFrameworkCore;

namespace Bmwadforth.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) {}

    public DbSet<Article> Articles { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .Property(b => b.CreatedDate)
            .HasDefaultValueSql("NOW()");
        
        modelBuilder.Entity<Article>()
            .Property(b => b.UpdatedDate)
            .HasDefaultValueSql("NOW()");
    }
}