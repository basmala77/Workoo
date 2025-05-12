using DataAcess;
using DataAcess.Repos.IRepos;

namespace IdentityManagerAPI.Repos.IRepos
{
    public interface IWorkerRepository :IRepository<Worker>
    {
        Task<IEnumerable<Worker>> GetTopRatedWorkers(int count);
        Task<IEnumerable<Worker?>> GetWorkerByCategory(string category);
    }
}
