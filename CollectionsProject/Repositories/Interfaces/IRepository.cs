using CollectionsProject.Context;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Save changes asynchronyously to database
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
