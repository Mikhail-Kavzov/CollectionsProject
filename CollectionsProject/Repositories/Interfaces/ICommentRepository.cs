using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Repositories.Interfaces
{
    public interface ICommentRepository : ICRUDRepository<Comment>
    {
        void AddUserComment(User user, Comment comment);
        Task<IEnumerable<Comment>> GetPreviousCommentsAsync(string itemId, DateTime time, int itemsToSkip, int ItemsToTake);
        Task<UserComment?> TryGetUserComment(string userId, Guid commentId);
        void AddUserComment(UserComment userComment);
        void UpdateUserComment(UserComment userComment);
        Task<int>CommentLikeCountAsync(Guid commentId);
    }
}
