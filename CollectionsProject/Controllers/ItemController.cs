using CollectionsProject.Extensions;
using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories.Interfaces;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IItemService _itemService;

        private const int itemCount = 5;

        public ItemController(ICollectionRepository collectionRepository,
            IItemRepository itemRepository, IItemService itemService)
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
            if (!this.HasAccess(collection.User.UserName))
                return Forbid();
            var model = _itemService.CreateItemViewModel(collection);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel model) //collectionId
        {
            if (ModelState.IsValid)
            {
                var collection = await _collectionRepository.GetItemIncludeFieldsAsync(model.CollectionId);
                if (collection == null)
                    return NotFound();
                if (!this.HasAccess(collection.User.UserName))
                    return Forbid();
                await _itemService.CreateNewItem(model, collection);
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ItemsPagination(string collectionId, int id = 0, 
            string sortRule = "Name", string searchString = "")
        {
            var items = await _itemRepository.Filter(id * itemCount, itemCount, collectionId, searchString);
            if (items != null) //skip sort if nothing was found
                items = _itemService.SortItems(items, sortRule);
            return PartialView(items);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _itemRepository.GetItemAsync(id);
            if (item == null)
                return NotFound();
            if (!this.HasAccess(item.Collection.User.UserName))
                return Forbid();
            _itemRepository.Delete(item);
            await _itemRepository.SaveChangesAsync();
            return Ok();
        }

        //item page with comments
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ItemPage(string id)
        {
            var item = await _itemService.GetAllItemFieldsAsync(id);
            if (item == null)
                return NotFound();
            ViewBag.CurrentUserName = "";
            if (User.Identity!.IsAuthenticated) //for displaying comment form
                ViewBag.CurrentUserName = User.Identity.Name;
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var item = await _itemService.GetAllItemFieldsAsync(id);
            if (item == null)
                return NotFound();
            if (!this.HasAccess(item.Collection.User.UserName))
                return Forbid();
            var itemViewModel = _itemService.CreateItemViewModel(item);
            return View(itemViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await _itemService.GetAllItemFieldsAsync(model.ItemId);
                if (item == null)
                    return NotFound();
                if (!this.HasAccess(item.Collection.User.UserName))
                    return Forbid();
                await _itemService.UpdateItem(model, item);
                return RedirectToAction("CollectionItems", "Collection", new { id = model.CollectionId });
            }
            return View(model);
        }

        //add new tag in update or create operations
        [HttpPost]
        public IActionResult AddTagToItem(int i = 0) => PartialView("TagPartial", i);
    }
}
