using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using BlogWebsite.IntegrationTests.Helpers;
using BlogWebsite.Common.Models;
using Newtonsoft.Json;
using BlogWebsite.Common.Interfaces;
using BlogWebsite.Common.Response;

namespace BlogWebsite.IntegrationTests.Controllers
{
    public class ArticleControllersTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ArticleControllersTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

        }

        [Fact]
        public async Task GetAllArticles_ShouldOnlyReturnPublishedArticles()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/article");
            var body = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<ApiResponse<List<ArticleDto>>>(body);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(1, json.Payload.Count());
        }
    }
}
