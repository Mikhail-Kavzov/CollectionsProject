using CollectionsProject.Models.ItemModels;

namespace CollectionsProject.Models.UserModels
{
    public class UserComment
    {
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public Guid CommentId { get; set; }
        public Comment Comment { get; set; } = null!;

        public bool IsLiked { get; set; }
    }
}
