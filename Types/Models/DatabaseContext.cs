using Microsoft.EntityFrameworkCore;

namespace Bmwadforth.Types.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

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