using FluentAssertions;
using Moq;
using StoreCenter.Application.Services;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.UnitTests.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockRepository;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            _service = new CategoryService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetCategoriesAsync_ShouldReturnCategories()
        {
            // Arrange
            var paginationOptions = new Domain.Dtos.PaginationOptions();
            
            // Mock repository setup would go here
            // _mockRepository.Setup(r => r.GetCategoriesAsync(paginationOptions))
            //     .ReturnsAsync(new List<Category>());

            // Act
            var result = await _service.GetCategoriesAsync(paginationOptions);

            // Assert
            result.Should().NotBeNull();
            _mockRepository.Verify(r => r.GetCategoriesAsync(paginationOptions), Times.Once);
        }
    }
}