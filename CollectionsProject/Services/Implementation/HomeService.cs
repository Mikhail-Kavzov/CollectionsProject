using CollectionsProject.Repositories;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Implementation
{
    public class HomeService : IHomeService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ITagRepository _tagRepository;

        public HomeService(ICollectionRepository collectionRepository, ITagRepository tagRepository, IItemRepository itemRepository)
        {
            _collectionRepository = collectionRepository;
            _itemRepository = itemRepository;
            _tagRepository = tagRepository;
        }

        public async Task<HomeModel> GetHomeModel(int countCollection = 5, int countItem = 5, int countTags = 20)
        {
            var collections = await _collectionRepository.GetLargestCollections(countCollection);
            var items = await _itemRepository.GetLastItemsAsync(countItem);
            var tags = await _tagRepository.GetTagList(countTags);
            return new HomeModel()
            {
                Collections = collections,
                Items = items,
                Tags = tags,
            };
        }
    }
}
