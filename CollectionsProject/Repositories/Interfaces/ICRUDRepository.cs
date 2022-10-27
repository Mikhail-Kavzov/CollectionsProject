namespace CollectionsProject.Repositories.Interfaces
{
    public interface ICRUDRepository<T> : IRepository<T>
    {
        /// <summary>
        /// Mark an element to create in database
        /// </summary>
        /// <param name="item">Element to create</param>
        void Create(T item);
        /// <summary>
        ///Mark an element to delete in database
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);
        /// <summary>
        /// Mark an element to update in database
        /// </summary>
        /// <param name="item"></param>
        void Update(T item);
    }
}
