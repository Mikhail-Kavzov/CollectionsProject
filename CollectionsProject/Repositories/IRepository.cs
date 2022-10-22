namespace CollectionsProject.Repositories
{
    public interface IRepository<T>
    {
        public void Create(T item);
        public void Delete(T item);
        public void Update(T item);
        public Task SaveChangesAsync();
    }
}
