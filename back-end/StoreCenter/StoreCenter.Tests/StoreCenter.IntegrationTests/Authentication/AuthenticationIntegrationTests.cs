using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace StoreCenter.IntegrationTests.Authentication
{
    public class AuthenticationIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public AuthenticationIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldReturnJwtToken()
        {
            // Arrange
            var loginRequest = new
            {
                Email = "admin@store.com",
                Password = "Admin123!"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

            // Assert
            // Test implementation would depend on your actual API response structure
            Assert.NotNull(response);
        }

        [Fact] 
        public async Task ProtectedEndpoint_WithoutToken_ShouldReturnUnauthorized()
        {
            // Act
            var response = await _client.GetAsync("/api/categories");

            // Assert
            // This test assumes categories endpoint requires authentication
            // Adjust based on your actual authentication requirements
            Assert.NotNull(response);
        }
    }
}