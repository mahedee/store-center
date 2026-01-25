using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace StoreCenter.IntegrationTests.Controllers
{
    public class CategoriesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public CategoriesControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetCategories_ShouldReturnOkStatus()
        {
            // Act
            var response = await _client.GetAsync("/api/categories");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnJsonContent()
        {
            // Act
            var response = await _client.GetAsync("/api/categories");

            // Assert
            response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
        }

        [Fact]
        public async Task GetCategories_ShouldAllowCorsFromAllowedOrigins()
        {
            // Arrange
            _client.DefaultRequestHeaders.Add("Origin", "http://localhost:3000");

            // Act
            var response = await _client.GetAsync("/api/categories");

            // Assert
            response.Headers.Should().Contain(h => h.Key == "Access-Control-Allow-Origin");
        }
    }
}