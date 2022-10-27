namespace CollectionsProject.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task SaveChangesAsync();
    }
}
