using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.ViewModels
{
    public class CommentViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string CommentId { get; set; } = string.Empty;
        public string ItemId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(300)]
        public string Text { get; set; } = string.Empty;
    }
}
