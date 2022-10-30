using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> CreateComment(CommentViewModel model);
        Task<IEnumerable<Comment>> GetNewComments(string itemId, string Time);
        Task<IEnumerable<Comment>> GetPreviousPage(string itemId, string time, int Page=0);
        Task<int> UpdateUserLike(string userId, Guid commentId, bool oldLikeState);
    }
}
