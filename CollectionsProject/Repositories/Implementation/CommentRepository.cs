using CollectionsProject.Context;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories.Implementation
{
    public class CommentRepository : AbstractRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext db) : base(db)
        {
        }

        //relations between user and comment for like
        public void AddUserComment(User user, Comment comment)
        {
            comment.Users.Add(user);
            comment.UserComments.Add(new UserComment()
            {
                IsLiked = false,
                Comment = comment,
                User = user,
            });
        }

        public void Create(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Delete(Comment item)
        {
            db.Comments.Remove(item);
        }

        //previous comments
        public async Task<IEnumerable<Comment>> GetPreviousCommentsAsync(string itemId, DateTime time,
            int itemsToSkip, int ItemsToTake)
        {
            return await db.Comments.Where(c => c.ItemId.ToString() == itemId && c.CreatedDate < time)
                .OrderByDescending(c => c.CreatedDate).Skip(itemsToSkip).Take(ItemsToTake)
                .Include(c => c.UserComments).ThenInclude(uc => uc.User).ToListAsync();
        }

        public void Update(Comment item)
        {
            db.Comments.Update(item);
        }

        //if there's a relation between user and comment (for like)
        public async Task<UserComment?> TryGetUserComment(string userId, Guid commentId)
        {
            return await db.UserComments.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CommentId == commentId);
        }

        public void AddUserComment(UserComment userComment)
        {
            db.UserComments.Add(userComment);
        }

        public void UpdateUserComment(UserComment userComment)
        {
            db.UserComments.Update(userComment);
        }

        //count likes for comment (get new comment)
        public async Task<int> CommentLikeCountAsync(Guid commentId)
        {
            return await db.UserComments.Where(uc=>uc.CommentId==commentId && uc.IsLiked==true).CountAsync();
        }
    }
}
