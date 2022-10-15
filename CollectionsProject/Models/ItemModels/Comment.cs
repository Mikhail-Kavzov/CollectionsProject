using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Models.ItemModels
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentText { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Author { get; set; } = "";

        public Guid ItemId { get; set; }
        public Item Item { get; set; } = null!;

        public List<UserComment> UserComments { get; set; } = new();
        public List<User> Users { get; set; } = new();
    }
}
