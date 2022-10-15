using CollectionsProject.Models.ItemModels;

namespace CollectionsProject.Repositories
{
    public interface ITagRepository:IRepository<Tag>
    {
        public Task<IEnumerable<string>> GetTagNamesAsync();
    }
}
