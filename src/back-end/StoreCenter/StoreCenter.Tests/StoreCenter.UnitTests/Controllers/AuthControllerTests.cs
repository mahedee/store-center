using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreCenter.Api.Controllers;
using StoreCenter.Application.Interfaces;

namespace StoreCenter.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnOkResult_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new Application.Dtos.LoginDto 
            { 
                Email = "test@example.com", 
                Password = "password" 
            };

            // Mock successful login
            _mockAuthService
                .Setup(s => s.LoginAsync(It.IsAny<Application.Dtos.LoginDto>()))
                .ReturnsAsync("fake-jwt-token");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Login_ShouldCallAuthService()
        {
            // Arrange
            var loginDto = new Application.Dtos.LoginDto 
            { 
                Email = "test@example.com", 
                Password = "password" 
            };

            // Act
            await _controller.Login(loginDto);

            // Assert
            _mockAuthService.Verify(s => s.LoginAsync(loginDto), Times.Once);
        }
    }
}