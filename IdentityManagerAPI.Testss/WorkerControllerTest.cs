using DataAcess;
using IdentityManagerAPI.Controllers;
using IdentityManagerAPI.ControllerService.IControllerService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IdentityManagerAPI.Testss
{
    public class WorkerControllerTest
    {
        private readonly Mock<IWorkerFacadeService> _workerFacadeService;
        private readonly WorkerController _workerController;

        public WorkerControllerTest()
        {
            _workerFacadeService = new Mock<IWorkerFacadeService>();
            _workerController = new WorkerController(_workerFacadeService.Object);
        }

        [Fact]
        public async Task GetWorkersByCategory_ReturnsOk_WhenWorkersExist()
        {
            //Arrange
            var worker = new List<Worker> { new Worker { Id = 1, Name = "Ali" } };
            _workerFacadeService.Setup(x => x.GetWorkersByCategory("Plumber"))
                .ReturnsAsync(worker);

            //Act
            var result = await _workerController.GetWorkersByCategory("Plumber");

            //Assert

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedWorkers = Assert.IsAssignableFrom<IEnumerable<Worker>>(okResult.Value);
            Assert.Single(returnedWorkers);
        }

        [Fact]
        public async Task GetWorkersByCategory_ReturnsNotFound_WhenNoWorkersExist()
        {
            //Arrange 
            var worker = new List<Worker>();
            _workerFacadeService.Setup(s => s.GetWorkersByCategory("plumber"))
                .ReturnsAsync(worker);

            //Act
            var result = await _workerController.GetWorkersByCategory("plumber");

            //Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No worker found in the category", notFoundResult.Value);
        }


        [Fact]
        public async Task GetTopRatedWorkers_ReturnsOk()
        {
            //Arrange
            var worker = new List<Worker> { new Worker { Id = 1, Name = "Ali",Rating = 4.9 }, new Worker() { Id = 2, Name = "Ahmed", Rating = 3.6} };
            _workerFacadeService.Setup(s => s.GetTopRatedWorkers(2)).ReturnsAsync(worker);

            //Act
            var result = await _workerController.GetTopRatedWorkers(2);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedWorkers = Assert.IsAssignableFrom<IEnumerable<Worker>>(okResult.Value);
            Assert.Equal(2, returnedWorkers.Count()); 

        }
        [Fact]
        public async Task AddWorker_ReturnsBadRequest_WhenWorkerIsNull()
        {
            // Arrange

            // Act
            var result = await _workerController.AddWorker(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid data.", badRequestResult.Value);
        }

        [Fact]
        public async Task AddWorker_ReturnsOk_WhenWorkerIsAddedSuccessfully()
        {
            // Arrange
          
            var newWorker = new Worker { Id = 1, Name = "Ali", Rating = 5 };
            _workerFacadeService.Setup(s => s.AddWorker(It.IsAny<Worker>())).ReturnsAsync(newWorker);


            // Act
            var result = await _workerController.AddWorker(newWorker);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedWorker = Assert.IsType<Worker>(okResult.Value);
            Assert.Equal("Ali", returnedWorker.Name);  
        }

    }
}
