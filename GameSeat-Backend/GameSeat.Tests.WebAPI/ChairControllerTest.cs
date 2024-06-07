using GameSeat.Backend.Infrastructure.Interfaces;
using GameSeat.Backend.WebAPI.Controllers;
using NUnit.Framework;
using Moq;

namespace GameSeat.Tests
{
    public class ChairsControllerTests
    {
        private readonly Mock<IChairService> _mockChairService;
        private readonly ChairsController _controller;

        public ChairsControllerTests()
        {
            _mockChairService = new Mock<IChairService>();
            _controller = new ChairsController(_mockChairService.Object);
        }

        [Fact]
        public async Task GetAllChairs_ReturnsOkResult_WithListOfChairs()
        {
            // Arrange
            var chairs = new List<ChairModel>
            {
                new ChairModel { Id = 1, Description = "Chair 1", Status = "Available", PricePerHour = 10.00M },
                new ChairModel { Id = 2, Description = "Chair 2", Status = "Unavailable", PricePerHour = 15.00M }
            };
            _mockChairService.Setup(service => service.GetAllChairsAsync()).ReturnsAsync(chairs);

            // Act
            var result = await _controller.GetAllChairs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnChairs = Assert.IsType<List<ChairModel>>(okResult.Value);
            Assert.Equal(2, returnChairs.Count);
        }

        [Fact]
        public async Task GetChair_ReturnsOkResult_WithChair()
        {
            // Arrange
            var chair = new ChairModel { Id = 1, Description = "Chair 1", Status = "Available", PricePerHour = 10.00M };
            _mockChairService.Setup(service => service.GetChairByIdAsync(1)).ReturnsAsync(chair);

            // Act
            var result = await _controller.GetChair(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnChair = Assert.IsType<ChairModel>(okResult.Value);
            Assert.Equal(1, returnChair.Id);
        }

        [Fact]
        public async Task GetChair_ReturnsNotFoundResult_WhenChairDoesNotExist()
        {
            // Arrange
            _mockChairService.Setup(service => service.GetChairByIdAsync(1)).ReturnsAsync((ChairModel)null);

            // Act
            var result = await _controller.GetChair(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateChair_ReturnsCreatedAtActionResult_WithChair()
        {
            // Arrange
            var chair = new ChairModel { Id = 1, Description = "Chair 1", Status = "Available", PricePerHour = 10.00M };

            // Act
            var result = await _controller.CreateChair(chair);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnChair = Assert.IsType<ChairModel>(createdAtActionResult.Value);
            Assert.Equal(1, returnChair.Id);
        }

        [Fact]
        public async Task UpdateChair_ReturnsNoContentResult()
        {
            // Arrange
            var chair = new ChairModel { Id = 1, Description = "Updated Chair", Status = "Available", PricePerHour = 12.00M };
            _mockChairService.Setup(service => service.UpdateChairAsync(chair, chair.Id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateChair(1, chair);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateChair_ReturnsBadRequestResult_WhenIdMismatch()
        {
            // Arrange
            var chair = new ChairModel { Id = 1, Description = "Updated Chair", Status = "Available", PricePerHour = 12.00M };

            // Act
            var result = await _controller.UpdateChair(2, chair);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteChair_ReturnsNoContentResult_WhenChairExists()
        {
            // Arrange
            var chair = new ChairModel { Id = 1, Description = "Chair 1", Status = "Available", PricePerHour = 10.00M };
            _mockChairService.Setup(service => service.GetChairByIdAsync(1)).ReturnsAsync(chair);
            _mockChairService.Setup(service => service.DeleteChairAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteChair(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteChair_ReturnsNotFoundResult_WhenChairDoesNotExist()
        {
            // Arrange
            _mockChairService.Setup(service => service.GetChairByIdAsync(1)).ReturnsAsync((ChairModel)null);

            // Act
            var result = await _controller.DeleteChair(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateChairStatus_ReturnsNoContentResult()
        {
            // Arrange
            _mockChairService.Setup(service => service.UpdateChairStatusAsync(1, true)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateChairStatus(1, true);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateChairStatus_ReturnsBadRequestResult_WhenExceptionIsThrown()
        {
            // Arrange
            _mockChairService.Setup(service => service.UpdateChairStatusAsync(1, true)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.UpdateChairStatus(1, true);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }
    }
}