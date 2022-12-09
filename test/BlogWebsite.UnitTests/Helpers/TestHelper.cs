using System.Data.Entity.Infrastructure;
using Moq;
using BlogWebsite.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MockQueryable.Moq;


namespace BlogWebsite.UnitTests.Helpers;

public class DatabaseContextFixture : IDisposable
{
    private static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new InMemoryDatabaseRoot();
    
    public DatabaseContext DatabaseContext { get; } = new(
        new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString(), InMemoryDatabaseRoot)
            .Options);

    public DatabaseContextFixture()
    {
        DatabaseContext.Articles.Add(new Article
        {
            ArticleId = 1, Title = "Test", Description = "Test", ContentId = Guid.Empty, ThumbnailId = Guid.Empty,
            Published = true
        });
        DatabaseContext.Articles.Add(new Article
        {
            ArticleId = 2, Title = "Test 2", Description = "Test 2", ContentId = Guid.Empty, ThumbnailId = Guid.Empty,
            Published = false
        });
        DatabaseContext.Users.Add(new User
        {
            UserId = 1, Username = "test", Password = "$2a$11$1Fy9dWPjk1Neb7l5XLgkNeY.ybt5jZO9Xw0wZ2EohWRmeg8gKnWh."
        });
        DatabaseContext.SaveChanges();
    }

    public void Dispose()
    {
        DatabaseContext.Dispose();
    }
}

public static class TestHelper
{
    public static IConfiguration GetConfig()
    {
        //TODO: Figure out a better way to mock IConfiguration
        var inMemorySettings = new Dictionary<string, string>
        {
            { "ApiKey", "Key" },
            { "Authentication:Audience", "bmwadforth.com" },
            { "Authentication:Issuer", "bmwadforth.com" },
            { "Authentication:Key", "key" },
            {
                "ConnectionStrings.Database",
                "User ID=admin;Password=password;Host=127.0.0.1;Port=5432;Database=bmwadforth;"
            },
            { "ConnectionStrings.ContentDeliveryNetwork", "https://cdn.bmwadforth.com/development" },
            { "Blob:Folder", "test" },
            { "Blob:Bucket", "bmwadforth" },
            { "AllowedHosts", "*" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        return configuration;
    }
}