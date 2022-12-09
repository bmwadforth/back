using System.Data.Entity.Infrastructure;
using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Models;
using BlogWebsite.Common.Repositories;
using BlogWebsite.UnitTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NSubstitute;
using Xunit;

namespace BlogWebsite.UnitTests.Repositories;

public class ArticleRepositoryTest
{
    private readonly IArticleRepository _sut;
    
    public ArticleRepositoryTest()
    {
        /*
        var data = new List<Article>
        {
            new() { ArticleId = 1, Title = "Test", ContentId = Guid.Empty, ThumbnailId = Guid.Empty, Published = true },
            new() { ArticleId = 2, Title = "Test 2", ContentId = Guid.Empty, ThumbnailId = Guid.Empty, Published = false },
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Article>>();
        mockSet.As<IDbAsyncEnumerable<Article>>()
            .Setup(m => m.GetAsyncEnumerator())
            .Returns(new TestDbAsyncEnumerator<Article>(data.GetEnumerator()));

        mockSet.As<IQueryable<Article>>()
            .Setup(m => m.Provider)
            .Returns(new TestDbAsyncQueryProvider<Article>(data.Provider));

        mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        var mockContext = new Mock<DatabaseContext>();
        mockContext.Setup(c => c.Articles).Returns(mockSet.Object);
        var blobRepository = Substitute.For<IBlobRepository>();
        var configuration = Substitute.For<IConfiguration>();
        
        _sut = new ArticleRepository(mockContext.Object, blobRepository, configuration);
        */
    }
    
    [Fact]
    public async void ArticleRepository_ReturnsArticles()
    {
        //var result = await _sut.GetArticles();

        //Assert.Single(result);
    }
}