using CollectionsProject.Models.UserModels;
using CollectionsProject.Repositories;
using CollectionsProject.Services.Interfaces;
﻿using CollectionsProject.Models;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Collection = CollectionsProject.Models.CollectionModels.Collection;

namespace CollectionsProject.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IFileService _fileService;
        private readonly ICollectionService _collectionService;
        private readonly UserManager<User> _userManager;
        private const int itemsCount = 5;
        private const string noPhoto = "noPhoto.jpg";

        public CollectionController(ICollectionRepository collectionRepository, UserManager<User> userManager, IFileService fileService,ICollectionService collectionService)
        {
            _collectionRepository = collectionRepository;
            _userManager = userManager;
            _fileService = fileService;
            _collectionService = collectionService;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create() => View();

        private async Task<User> CheckCurrentUser(string name)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                throw new ArgumentNullException($"No user with name {name}");
            return currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollectionViewModel model, IFormFile? formFile = null)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await CheckCurrentUser(model.Name);
                model.Image = noPhoto;
                if (formFile != null)
                    model.Image = await _fileService.CreateFileAsync(formFile);
                var collection = _collectionService.CreateNewCollection(model, currentUser);
                _collectionRepository.Create(collection);

                var fields = _collectionService.CreateAddFields(model.CustomFields, collection);
                _collectionRepository.AddFieldsRange(fields);
                await _collectionRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private static Collection CreateNewCollection(CollectionViewModel model,User user)
        {
            Collection collection = new()
            {
                Image = model.Image,
                Type = model.Type,
                Description = model.Description,
                Name = model.Name,
                User = user,
                Count = 0,
            };
            return collection;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var collection = await _collectionRepository.GetItemAsync(id);
            _collectionRepository.Delete(collection);
            if (collection.Image != noPhoto)
                _fileService.DeleteFile(collection.Image);
            await _collectionRepository.SaveChangesAsync();
            return new OkResult();
        }

        [HttpGet]
        public IActionResult Update() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Update(CollectionViewModel model, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                var collection = await _collectionRepository.GetItemAsync(model.Id);

                collection.Description = model.Description;
                collection.Name = model.Name;
                collection.Type = model.Type;

                if (formFile != null) //previous file will be remained
                    collection.Image = await _fileService.UpdateFileAsync(formFile, collection.Image);
                _collectionRepository.Update(collection);
                await _collectionRepository.SaveChangesAsync();
                return PartialView("CollectionPage", new List<Collection>() { collection });
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> CollectionPage(int id = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            var collections = await _collectionRepository.GetUserItemsAsync(id * itemsCount, itemsCount,user.Id);
            return PartialView("CollectionPage", collections);
        }

        [HttpGet]
        public async Task<IActionResult> CollectionItems(string id)
        {
            var result = await _collectionRepository.GetItemAsync(id);
            if (result == null)
                return NotFound();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCollectionPage(int Page = 0)
        {
            var collections = await _collectionRepository.GetSomeItemsAsync(Page * itemsCount, itemsCount);
            return PartialView("CollectionPage",collections);
        }
    }
}
