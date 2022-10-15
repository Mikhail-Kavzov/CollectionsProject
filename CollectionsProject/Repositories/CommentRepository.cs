using CollectionsProject.Context;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext db;

        public CommentRepository(ApplicationContext db)
        {
            this.db = db;
        }

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

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await db.Comments.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTimeAsync(DateTime time, string itemId)
        {
            return await db.Comments.Where(c => c.ItemId.ToString() == itemId && c.CreatedDate > time)
                .Include(c => c.UserComments).Include(c => c.Users).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await db.Comments.CountAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(Comment item)
        {
            db.Comments.Update(item);
        }
    }
}
