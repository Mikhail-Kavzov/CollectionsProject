using CollectionsProject.Models.ItemModels;

namespace CollectionsProject.Models.UserModels
{
    public class UserComment
    {
        public string? UserId { get; set; }
        public User? User { get; set; }

        public Guid CommentId { get; set; }
        public Comment? Comment { get; set; }

        public bool IsLiked { get; set; }
    }
}
