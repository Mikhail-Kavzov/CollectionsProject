using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Repositories
{
    public interface ICommentRepository:IRepository<Comment>
    {
        void AddUserComment(User user, Comment comment);
        Task<IEnumerable<Comment>> GetCommentsByTimeAsync(DateTime time, string itemId);
    }
}
