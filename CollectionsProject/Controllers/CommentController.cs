using CollectionsProject.Models.UserModels;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentService commentService, UserManager<User> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                await _commentService.CreateComment(currentUser, model);
            }
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CommentPage(string itemId, string Time)
        {
            var comments = await _commentService.GetNewComments(itemId, Time);
            return PartialView(comments);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PreviousPage(string itemId, string Time, int Page = 0)
        {
            var comments = await _commentService.GetPreviousPage(itemId, Time, Page);
            return PartialView("CommentPage", comments);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLike(string commentId, bool oldLikeState)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await _commentService.UpdateUserLike(currentUser.Id, Guid.Parse(commentId), oldLikeState);
            return Ok();
        }
    }
}
