using CollectionsProject.Context;
using CollectionsProject.Repositories.Interfaces;
using System.Collections.ObjectModel;

namespace CollectionsProject.Repositories.Implementation
{
    public abstract class AbstractRepository<T> : IRepository<T>
    {
        protected readonly ApplicationContext db;

        protected AbstractRepository(ApplicationContext db)
        {
            this.db = db;
        }

        //save changes to database
        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
