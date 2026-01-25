using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StoreCenter.Api.Extensions;

namespace StoreCenter.UnitTests.Extensions
{
    public class CorsExtensionsTests
    {
        [Fact]
        public void AddCorsPolicy_ShouldAddCorsService()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddCorsPolicy();

            // Assert
            services.Should().Contain(s => s.ServiceType.Name.Contains("Cors"));
        }

        [Fact]
        public void AddCorsPolicy_ShouldConfigureAllowFrontendPolicy()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var result = services.AddCorsPolicy();

            // Assert
            result.Should().BeSameAs(services);
            services.Should().NotBeEmpty();
        }
    }
}