using CollectionsProject.Converter;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories;
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

        private static Comment CreateComment(CommentViewModel model)
        {
            Comment comment = new()
            {
                CommentText = model.Text,
                CreatedDate = DateTime.Now,
                Author = model.UserName,
                ItemId = Guid.Parse(model.ItemId),
            };
            return comment;
        }

        public async Task CreateComment(User user, CommentViewModel model)
        {
            model.UserName = user.UserName;
            var comment = CreateComment(model);
            _commentRepository.Create(comment);
            _commentRepository.AddUserComment(user, comment);
            await _commentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetNewComments(string itemId, string Time)
        {
            var time = TimeConverter.ConvertFromUTCTime(Time);
            return await _commentRepository.GetCommentsByTimeAsync(time, itemId);
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

        public async Task UpdateUserLike(string userId, Guid commentId, bool oldLikeState)
        {
            var userComment = await _commentRepository.TryGetUserComment(userId, commentId);
            if (userComment == null) // if there's no relations between user and comment - create new UserComment
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
        }
    }
}
