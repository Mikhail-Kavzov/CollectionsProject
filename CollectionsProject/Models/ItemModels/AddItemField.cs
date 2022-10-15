using CollectionsProject.Models.CollectionModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionsProject.Models.ItemModels
{
    public class AddItemField
    {
        public Guid AddItemFieldId { get; set; }
        public string Value { get; set; } = "";

        public Guid ItemId { get; set; }
        public Item Item { get; set; } = null!;

        [ForeignKey(nameof(AddCollectionFields))]
        public Guid FieldId { get; set; }
        public AddCollectionField AddCollectionFields { get; set; } = null!;
    }
}
