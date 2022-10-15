namespace CollectionsProject.Models.ItemModels
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string TagName { get; set; } = "";

        public List<Item> Items { get; set; } = null!;
    }
}
