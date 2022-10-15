using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;

namespace CollectionsProject.Models.CollectionModels
{
    public class Collection
    {
        public Guid CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public CollectionType Type { get; set; }
        public int Count { get; set; }

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<Item>? Items { get; set; }
        public List<AddCollectionField>? AddFields { get; set; }
    }
}
