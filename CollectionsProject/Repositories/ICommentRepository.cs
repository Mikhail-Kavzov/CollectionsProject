using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        void AddUserComment(User user, Comment comment);
        Task<IEnumerable<Comment>> GetCommentsByTimeAsync(DateTime time, string itemId);
        Task<IEnumerable<Comment>> GetPreviousCommentsAsync(string itemId, DateTime time, int itemsToSkip, int ItemsToTake);
        Task<UserComment?> TryGetUserComment(string userId, Guid commentId);
        void AddUserComment(UserComment userComment);
        void UpdateUserComment(UserComment userComment);
    }
}
