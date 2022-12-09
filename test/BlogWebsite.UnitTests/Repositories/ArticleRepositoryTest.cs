using BlogWebsite.Common.Exceptions;
using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Models;
using BlogWebsite.Common.Repositories;
using BlogWebsite.Common.Request;
using BlogWebsite.UnitTests.Helpers;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace BlogWebsite.UnitTests.Repositories;

public class ArticleRepositoryTest : IClassFixture<DatabaseContextFixture>
{
    private readonly IArticleRepository _sut;
    
    public ArticleRepositoryTest(DatabaseContextFixture fixture)
    {
        var blobRepository = Substitute.For<IBlobRepository>();
        var configuration = Substitute.For<IConfiguration>();

        _sut = new ArticleRepository(fixture.DatabaseContext, blobRepository, configuration);
    }
    
    [Fact]
    public async void ArticleRepository_ReturnsArticles()
    {
        var result = await _sut.GetArticles();
        
        Assert.Equal("Test", result[0].Title);
        Assert.True(result[0].Published);
    }
    
    [Fact]
    public async void ArticleRepository_CreatesArticles()
    {
        await _sut.NewArticle(new CreateArticleDto { Title = "Test 3", Description = "Test 3", Content = Guid.Empty, Thumbnail = Guid.Empty });
        await _sut.PublishArticle(3);
        var result = await _sut.GetArticles();

        Assert.True(result.Where(article => article.ArticleId == 3).Select(article => article.Published).First());
    }
    
    [Fact]
    public async Task ArticleRepository_ThrowsNotFoundExceptionOnGet()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () => await _sut.GetArticle(10000));
    }
}