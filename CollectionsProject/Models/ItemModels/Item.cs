using CollectionsProject.Models.CollectionModels;

namespace CollectionsProject.Models.ItemModels
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Guid CollectionId { get; set; }
        public Collection Collection { get; set; } = null!;

        public List<AddItemField>? AddItems { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Tag> Tags { get; set; } = null!;
    }
}
