using CollectionsProject.Converter;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentRepository commentRepository, UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
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

        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                model.UserName = currentUser.UserName;
                var comment = CreateComment(model);
                _commentRepository.Create(comment);
                _commentRepository.AddUserComment(currentUser, comment);
                await _commentRepository.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CommentPage(string itemId, string Time)
        {
            var time = TimeConverter.ConvertFromUTCTime(Time);
            var comments = await _commentRepository.GetCommentsByTimeAsync(time, itemId);
            return PartialView(comments);
        }
    }
}
