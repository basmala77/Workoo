using DataAcess.Repos;
using DataAcess;
using DataAcess.Repos.IRepos;
using IdentityManagerAPI.Repos.IRepos;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagerAPI.Repos
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        private readonly ApplicationDbContext _db;

        public WorkerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public async Task<IEnumerable<Worker>> GetTopRatedWorkers(int count)
        {
            return await _db.Workers.OrderByDescending(w => w.Rating)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Worker?>> GetWorkerByCategory(string category)
        {
            return await _db.Workers
                .Where(w => w.Specialty == category)
                .ToListAsync();
        }
    }
}
