using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreCenter.Api.Controllers;
using StoreCenter.Application.Interfaces;

namespace StoreCenter.UnitTests.Controllers
{
    public class CategoriesControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new CategoriesController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnOkResult_WhenCategoriesExist()
        {
            // Arrange
            // Mock service setup would go here

            // Act
            var result = await _controller.GetCategories(new Domain.Dtos.PaginationOptions());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetCategories_ShouldCallCategoryService()
        {
            // Arrange
            var paginationOptions = new Domain.Dtos.PaginationOptions();

            // Act
            await _controller.GetCategories(paginationOptions);

            // Assert
            _mockCategoryService.Verify(s => s.GetCategoriesAsync(paginationOptions), Times.Once);
        }
    }
}