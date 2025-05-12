using Moq;
using Microsoft.AspNetCore.Mvc;
using IdentityManagerAPI.Controllers;
using IdentityManagerAPI.ControllerService.IControllerService;
using DataAcess;

namespace IdentityManagerAPI.Testss

{
    public class WorkerControllerTests
    {
        [Fact]
        public async Task GetWorkersByCategory_ReturnsOk_WhenWorkersExist()
        {
            // Arrange
            var mockService = new Mock<IWorkerFacadeService>();
            mockService.Setup(service => service.GetWorkersByCategory("Plumber"))
                       .ReturnsAsync(new List<Worker> {
                       new Worker { Id = 1, Name = "Omar", Specialty = "Plumber" }
                       });

            var controller = new WorkerController(mockService.Object);

            // Act
            var result = await controller.GetWorkersByCategory("Plumber");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedWorkers = Assert.IsType<List<Worker>>(okResult.Value);
            Assert.Single(returnedWorkers);
        }

        [Fact]
        public async Task GetTopRatedWorkers_ReturnsOk_WhenWorkersExist()
        {
            // Arrange
            var mockService = new Mock<IWorkerFacadeService>();
            mockService.Setup(service => service.GetTopRatedWorkers(3))
                       .ReturnsAsync(new List<Worker> {
                   new Worker { Id = 1, Name = "Omar", Rating = 5 }
                       });

            var controller = new WorkerController(mockService.Object);

            // Act
            var result = await controller.GetTopRatedWorkers(3);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedWorkers = Assert.IsType<List<Worker>>(okResult.Value);
            Assert.Single(returnedWorkers);
        }
        [Fact]
        public async Task AddWorker_ReturnsOk_WhenWorkerIsAdded()
        {
            // Arrange
            var mockService = new Mock<IWorkerFacadeService>();
            var newWorker = new Worker { Id = 1, Name = "Ahmed", Specialty = "Plumber", Rating = 5 };
            mockService.Setup(service => service.AddWorker(It.IsAny<Worker>()))
                       .ReturnsAsync(newWorker);

            var controller = new WorkerController(mockService.Object);

            // Act
            var result = await controller.AddWorker(newWorker);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var addedWorker = Assert.IsType<Worker>(okResult.Value);
            Assert.Equal(newWorker.Name, addedWorker.Name);
        }

        [Fact]
        public async Task GetWorkerById_ReturnsOk_WhenWorkerExists()
        {
            // Arrange
            var mockService = new Mock<IWorkerFacadeService>();
            var existingWorker = new Worker { Id = 1, Name = "Ahmed", Specialty = "Plumber", Rating = 5 };
            mockService.Setup(service => service.GetWorkerById(1))
                       .ReturnsAsync(existingWorker); // محاكاة لإرجاع العامل عندما نبحث عنه بالـ Id

            var controller = new WorkerController(mockService.Object);

            // Act
            var result = await controller.GetWorker(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // التأكد من أن النتيجة هي OkObjectResult
            var worker = Assert.IsType<Worker>(okResult.Value); // التأكد من أن العامل الذي تم إرجاعه هو من نوع Worker
            Assert.Equal(existingWorker.Name, worker.Name); // التأكد من أن اسم العامل الذي تم إرجاعه هو نفسه الذي طلبناه
        }

        [Fact]
        public async Task DeleteWorker_ReturnsOk_WhenWorkerIsDeleted()
        {
            // Arrange
            var mockService = new Mock<IWorkerFacadeService>();
            mockService.Setup(service => service.DeleteWorker(1))
                       .ReturnsAsync(true); // محاكاة إرجاع true للدلالة على أن العامل تم حذفه بنجاح

            var controller = new WorkerController(mockService.Object);

            // Act
            var result = await controller.DeleteWorker(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // التأكد من أن النتيجة هي OkObjectResult بدلاً من OkResult
            Assert.Equal(200, okResult.StatusCode); // التأكد من أن الـ Status Code هو 200 (نجاح)
        }
        [Fact]
        public async Task UpdateWorker_ReturnsOk_WhenWorkerIsUpdated()
        {
            // Arrange
            var mockService = new Mock<IWorkerFacadeService>();
            var updatedWorker = new Worker { Id = 1, Name = "Ahmed", Specialty = "Plumber", Rating = 4 }; // التحديث
            mockService.Setup(service => service.UpdateWorker(1, It.IsAny<Worker>()))
                       .ReturnsAsync(updatedWorker);

            var controller = new WorkerController(mockService.Object);

            // Act
            var result = await controller.UpdateWorker(1, updatedWorker);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // التأكد من أن النتيجة هي OkObjectResult
            var worker = Assert.IsType<Worker>(okResult.Value); // التأكد من أن العامل الذي تم تحديثه هو من نوع Worker
            Assert.Equal(updatedWorker.Rating, worker.Rating); // التأكد من أن التقييم المحدث هو نفسه الذي أرسلناه
        }

        [Fact]
        public async Task GetAllWorkers_ReturnsOk_WhenWorkersExist()
        {
            // Arrange
            var mockService = new Mock<IWorkerFacadeService>();
            var workersList = new List<Worker>
            {
                new Worker { Id = 1, Name = "Ahmed", Specialty = "Plumber", Rating = 5 },
                new Worker { Id = 2, Name = "Omar", Specialty = "Electrician", Rating = 4 }
            };

            // إعداد الـ mock ليُرجع قائمة من العمال
            mockService.Setup(service => service.GetAllWorkers())
                .ReturnsAsync(workersList);

            var controller = new WorkerController(mockService.Object);

            // Act
            var result = await controller.GetAllWorkers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // التأكد من أن النتيجة هي OkObjectResult
            var returnedWorkers = Assert.IsType<List<Worker>>(okResult.Value); // التأكد من أن النتيجة هي List<Worker>
            Assert.Equal(2, returnedWorkers.Count); // التأكد من أن القائمة تحتوي على اثنين من العمال
        }
    }
}