namespace CollectionsProject.Repositories
{
    public interface IRepository<T>
    {
        void Create(T item);
        void Delete(T item);
        void Update(T item);
        Task SaveChangesAsync();
    }
}
