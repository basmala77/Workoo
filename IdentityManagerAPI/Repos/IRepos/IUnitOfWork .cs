namespace IdentityManagerAPI.Repos.IRepos
{
    public interface IUnitOfWork
    {
        IWorkerRepository Workers { get; }
        Task SaveAsync();
    }
}
