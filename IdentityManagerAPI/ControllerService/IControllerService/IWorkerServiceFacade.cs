using DataAcess;
using IdentityManagerAPI.Controllers;
using System.Threading.Tasks;

namespace IdentityManagerAPI.ControllerService.IControllerService
{
    /// <summary>
    /// /////////
    /// </summary>
    public interface IWorkerFacadeService
    {
        Task<IEnumerable<Worker?>> GetWorkersByCategory(string category);
        Task<IEnumerable<Worker>> GetTopRatedWorkers(int count);
        Task<IEnumerable<Worker>> GetWorkersByCategoryWithNear(string category, double userLat, double userLon,string sorting);
        Task<bool> HandleServiceRequest(ServiceRequest request);
        Task<Worker?> GetWorkerById(int id);
        Task<IEnumerable<Worker?>> GetAllWorkers();
        Task<Worker?> AddWorker(Worker? worker);
        Task<Worker?> UpdateWorker(int id, Worker updatedWorker);
        Task<bool> DeleteWorker(int id);
        Task<List<string>> GetAllSpecialties();
        Task<(bool isSuccess, string message)> HandleServiceRequestWithNotification(ServiceRequest request);
    }

}
