using CollectionsProject.Models.UserModels;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace CollectionsProject
{
    public class CommentHub : Hub
    {
        private readonly ICommentService _commentService;

        public CommentHub(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //subscribe to comments in item
        public async Task SubscribeComment(string itemId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, itemId);
        }

        // create comment
        [Authorize]
        public async Task CreateComment(CommentViewModel model)
        {
            var comment = await _commentService.CreateComment(model);
            await Clients.Group(model.ItemId.ToString()).SendAsync("GetNewComment", comment);
        }
    }
}
