using DataAcess;
using IdentityManagerAPI.Controllers;
using IdentityManagerAPI.ControllerService.IControllerService;
using IdentityManagerAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IdentityManagerAPI.Testss;

public class ServiceRequestsControllerTests
{
    private readonly Mock<IWorkerFacadeService> _workerFacadeMock;
    private readonly ServiceRequestsController _controller;
    
    public ServiceRequestsControllerTests()
    {
        _workerFacadeMock = new Mock<IWorkerFacadeService>();
        var notificationServiceMock = new Mock<INotificationService>(); 
        _controller = new ServiceRequestsController(_workerFacadeMock.Object, notificationServiceMock.Object);
    }

    [Fact]
    public async Task RequestService_ReturnsOk_WhenSuccess()
    {
        // Arrange
        var request = new ServiceRequest(); 
        _workerFacadeMock
            .Setup(x => x.HandleServiceRequestWithNotification(request))
            .ReturnsAsync((true, "Request sent successfully"));

        // Act
        var result = await _controller.RequestService(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(okResult.Value);

        Assert.True(response.Success);
    }

    [Fact]
    public async Task RequestService_ReturnsBadRequest_WhenFailed()
    {
        // Arrange
        var request = new ServiceRequest(); // Populate as needed
        _workerFacadeMock
            .Setup(x => x.HandleServiceRequestWithNotification(request))
            .ReturnsAsync((false, "Failed"));

        // Act
        var result = await _controller.RequestService(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Failed", badRequestResult.Value);
    }

    [Fact]
    public async Task GetAllService_ReturnsList()
    {
        // Arrange
        var mockList = new List<string> { "Plumbing", "Electrician" };
        _workerFacadeMock
            .Setup(x => x.GetAllSpecialties())
            .ReturnsAsync(mockList);

        // Act
        var result = await _controller.GetAllService();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mockList, okResult.Value);
    }
    [Fact]
    public async Task GetNearbyWorker_ReturnsWorkers()
    {
        // Arrange
        var mockWorkers = new List<Worker>
        {
            new Worker { Id = 1, Name = "Ali" },
            new Worker { Id = 2, Name = "Ahmed" },
            new Worker { Id = 3, Name = "Mona" }
        }.AsEnumerable(); 

        _workerFacadeMock
            .Setup(x => x.GetWorkersByCategoryWithNear("Plumbing", 30.1, 31.2, "distance"))
            .ReturnsAsync(mockWorkers);

        // Act
        var result = await _controller.GetNearbyWorker("Plumbing", 30.1, 31.2, "distance");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var returnedWorkers = Assert.IsAssignableFrom<IEnumerable<Worker>>(okResult.Value);
        Assert.Equal(3, returnedWorkers.Count());
    }

}