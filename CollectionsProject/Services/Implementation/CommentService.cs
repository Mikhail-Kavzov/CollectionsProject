using CollectionsProject.Converter;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories.Interfaces;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CollectionsProject.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private const int pageCount = 5;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        private static Comment CreateCommentData(CommentViewModel model)
        {
            Comment comment = new()
            {
                CommentText = model.Text,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                Author = model.UserName,
                ItemId = Guid.Parse(model.ItemId),
            };
            return comment;
        }

        public async Task<Comment> CreateComment(CommentViewModel model)
        {
            var comment = CreateCommentData(model);
            _commentRepository.Create(comment);
            await _commentRepository.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetPreviousPage(string itemId, string time, int Page = 0)
        {
            var Time = TimeConverter.ConvertFromUTCTime(time);
            var comments = await _commentRepository.GetPreviousCommentsAsync(itemId, Time, Page * pageCount, pageCount);
            return comments.Reverse();
        }

        private static UserComment CreateUserComment(string userId, Guid commentId)
        {
            UserComment userComment = new()
            {
                IsLiked = false,
                UserId = userId,
                CommentId = commentId,
            };
            return userComment;
        }

        public async Task<int> UpdateUserLike(string userId, Guid commentId, bool oldLikeState)
        {
            var userComment = await _commentRepository.TryGetUserComment(userId, commentId);
            if (userComment == null) // if there are no relations between user and comment - create new UserComment
            {
                userComment = CreateUserComment(userId, commentId);
                userComment.IsLiked = true;
                _commentRepository.AddUserComment(userComment);
            }
            else
            {
                userComment.IsLiked = !oldLikeState;
                _commentRepository.UpdateUserComment(userComment);
            }
            await _commentRepository.SaveChangesAsync();
            return await _commentRepository.CommentLikeCountAsync(commentId);
        }
    }
}
