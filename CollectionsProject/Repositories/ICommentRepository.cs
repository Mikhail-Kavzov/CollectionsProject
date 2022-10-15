using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Repositories
{
    public interface ICommentRepository:IRepository<Comment>
    {
        public void AddUserComment(User user, Comment comment);
        public Task<IEnumerable<Comment>> GetCommentsByTimeAsync(DateTime time, string itemId);
    }
}
