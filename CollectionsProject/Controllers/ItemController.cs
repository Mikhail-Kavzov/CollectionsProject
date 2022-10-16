﻿using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Repositories;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public ItemController(ICollectionRepository collectionRepository, IItemRepository itemRepository, IItemService itemService)
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
                await _itemService.CreateNewItem(model,collection);
            }
            return View(model);
        }

        [HttpPost]
        [Route("{collectionId}/{id?}")]
        [AllowAnonymous]
        public async Task<IActionResult> ItemsPagination(string collectionId, int id = 0)
        {
            var items = await _itemRepository.GetUserItemsAsync(id * itemCount, itemCount, collectionId);
            return PartialView(items);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _itemRepository.GetItemAsync(id);
            if (item == null)
                return NotFound();
            _itemRepository.Delete(item);
            await _itemRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ItemPage(string id)
        {
            var item = await _itemService.GetAllItemFieldsAsync(id);
            if (item == null)
                return NotFound();
            return View(item);
        }    

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var item = await _itemService.GetAllItemFieldsAsync(id);           
            if (item == null)
                return NotFound();
            var itemViewModel=_itemService.CreateItemViewModel(item);
            return View(itemViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item=await _itemService.GetAllItemFieldsAsync(model.ItemId);
                await _itemService.UpdateItem(model, item);
                return RedirectToAction("CollectionItems","Collection", new { id = model.CollectionId });
            }
            return View(model);
        }
    }
}
