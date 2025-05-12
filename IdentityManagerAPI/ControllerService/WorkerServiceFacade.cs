using Amazon.Util.Internal.PlatformServices;
using DataAcess;
using IdentityManager.Services.ControllerService.IControllerService;
using IdentityManagerAPI.Controllers;
using IdentityManagerAPI.ControllerService.IControllerService;
using IdentityManagerAPI.Repos.IRepos;
using Microsoft.EntityFrameworkCore;
using ServiceFactory = IdentityManager.Services.ControllerService.IControllerService.ServiceFactory;

namespace IdentityManagerAPI.ControllerService
{
    /// <summary>
    /// Facade class that manages worker-related operations 
    /// and interacts with multiple services like NotificationService and Repository.
    /// </summary>
    public class WorkerFacadeService : IWorkerFacadeService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ServiceFactory _serviceFactory;
        private readonly DistanceStrategyFactory _strategyFactory;
        private readonly ApplicationDbContext _context;
        private readonly NotificationService _notificationService;


        public WorkerFacadeService(
            IWorkerRepository workerRepository,
         
            ServiceFactory serviceFactory,
            DistanceStrategyFactory strategyFactory,
            ApplicationDbContext context,
            NotificationService notificationService)
        {
            _workerRepository = workerRepository;
            _serviceFactory = serviceFactory;
            _strategyFactory = strategyFactory;
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<Worker>> GetTopRatedWorkers(int count)
        {
            return await _workerRepository.GetTopRatedWorkers(count);
        }
        public async Task<IEnumerable<Worker?>> GetWorkersByCategory(string category)
        {
            return await _workerRepository.GetWorkerByCategory(category);
        }


        public async Task<IEnumerable<Worker>> GetWorkersByCategoryWithNear(
            string category,
            double userLat,
            double userLon,
            string sortBy)
        {
            var strategy = _strategyFactory.GetStrategy(sortBy);
            var workers = await _workerRepository.GetWorkerByCategory(category);

            return workers
                .OrderByDescending(w => strategy.CalculateSorting(w, userLat, userLon))
                .ToList();
        }

        public async Task<bool> HandleServiceRequest(ServiceRequest request)
        {
            var service = _serviceFactory.CreateService(request.ServiceType);
            if (service == null)
                return false;
            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Worker?> GetWorkerById(int id)
        {
            return await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<Worker?>> GetAllWorkers()
        {
            return await _context.Workers.ToListAsync();
        }

        public async Task<Worker?> AddWorker(Worker? worker)
        {
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<Worker?> UpdateWorker(int id, Worker updatedWorker)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(x => x.Id == id);
            if (worker == null) return null;

            worker.Name = updatedWorker.Name;
            worker.Rating = updatedWorker.Rating;
            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<bool> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);
            if (worker == null) return false;

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<string>> GetAllSpecialties()
        {
            return await _context.Workers
                                 .Select(w => w.Specialty)
                                 .Distinct()
                                 .ToListAsync();
        }
        public async Task<(bool isSuccess, string message)> HandleServiceRequestWithNotification(ServiceRequest request)
        {
            var service = _serviceFactory.CreateService(request.ServiceType);
            if (service == null)
                return (false, "Unsupported service type");

            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotification("New Order :)");

            return (true, "Order is sent");
        }
    }
}
