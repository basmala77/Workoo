using DataAcess;
using IdentityManagerAPI.Repos.IRepos;

namespace IdentityManagerAPI.Repos
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IWorkerRepository Workers { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Workers = new WorkerRepository(db);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
