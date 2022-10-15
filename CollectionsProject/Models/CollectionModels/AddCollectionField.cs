
using CollectionsProject.Models.ItemModels;
using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.Models.CollectionModels
{
    public class AddCollectionField
    {
        [Key]
        public Guid FieldId { get; set; }
        public string Name { get; set; } = "";
        public CollectionFieldType Type { get; set; }

        public Guid CollectionId { get; set; }
        public Collection Collection { get; set; } = null!;

        public List<AddItemField>? AddItems { get; set; }
    }
}
