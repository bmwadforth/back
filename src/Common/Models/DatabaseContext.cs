using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Common.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .HasIndex(p => new { p.ArticleId, p.Title })
            .IsUnique();

        modelBuilder.Entity<Article>()
            .Property(b => b.CreatedDate)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Article>()
            .Property(b => b.UpdatedDate)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Project>()
            .HasIndex(p => new { p.ProjectId, p.Title })
            .IsUnique();

        modelBuilder.Entity<Project>()
            .Property(b => b.CreatedDate)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Project>()
            .Property(b => b.UpdatedDate)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<User>()
            .HasIndex(p => new { p.UserId })
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(b => b.CreatedDate)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<User>()
            .Property(b => b.UpdatedDate)
            .HasDefaultValueSql("NOW()");
    }
}