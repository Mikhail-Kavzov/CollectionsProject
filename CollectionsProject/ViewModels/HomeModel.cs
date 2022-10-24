using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;

namespace CollectionsProject.ViewModels
{
    public class HomeModel
    {
        public IEnumerable<Collection>? Collections { get; set; }
        public IEnumerable<string>? Tags { get; set; }
        public IEnumerable<Item>? Items { get; set; }
    }
}
