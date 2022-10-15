using CollectionsProject.Repositories;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IItemService _itemService;

        private const int itemCount = 10;

        public ItemController(ICollectionRepository collectionRepository, IItemRepository itemRepository,IItemService itemService)
        {
            _collectionRepository = collectionRepository;
            _itemRepository = itemRepository;
            _itemService = itemService;
        }       

        [HttpGet]
        public async Task<IActionResult> Create(string id) //collectionId
        {
            var collection = await _collectionRepository.GetItemIncludeFieldsAsync(id);
            if (collection == null)
                return NotFound();
            var model = _itemService.CreateItemViewModel(collection);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel model) //collectionId
        {
            if (ModelState.IsValid)
            {
                var collection = await _collectionRepository.GetItemIncludeFieldsAsync(model.CollectionId);
                var tags = _itemService.CreateTags(model.Tags);
                _itemRepository.AddTagRange(tags);
                var item = _itemService.CreateNewItem(model.Name, collection, tags);
                _itemRepository.Create(item);
                var addFields = _itemService.CreateFields(model.AddItems, collection.AddFields, item);
                _itemRepository.AddFieldRange(addFields);
                await _itemRepository.SaveChangesAsync();
            }
            return View(model);
        }

        [HttpPost]
        [Route("{collectionId}/{id?}")]
        public async Task<IActionResult> ItemsPagination(string collectionId, int id = 0)
        {
            var items = await _itemRepository.GetUserItemsAsync(id * itemCount, itemCount, collectionId);
            return PartialView(items);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var item= await _itemRepository.GetItemAsync(id);
            if (item == null)
                return NotFound();
            _itemRepository.Delete(item);
            await _itemRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
