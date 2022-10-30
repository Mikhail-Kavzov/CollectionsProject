using CollectionsProject.Models.UserModels;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Collection = CollectionsProject.Models.CollectionModels.Collection;
using CollectionsProject.Repositories.Interfaces;
using CollectionsProject.Extensions;

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

        public CollectionController(ICollectionRepository collectionRepository, UserManager<User> userManager, IFileService fileService, ICollectionService collectionService)
        {
            _collectionRepository = collectionRepository;
            _userManager = userManager;
            _fileService = fileService;
            _collectionService = collectionService;
        }

        //Personal page of collection
        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create() => View();

        //List of collections to view
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CollectionList() => View();

        //Add field to new collection
        [HttpGet]
        public IActionResult AddNewField(int i = 0) => PartialView("AddNewField", i);

        [HttpPost]
        public async Task<IActionResult> Create(CollectionViewModel model, IFormFile? formFile = null)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                model.Image = noPhoto;
                if (formFile != null) //user uploaded the image
                    model.Image = await _fileService.CreateFileAsync(formFile);
                var collection = _collectionService.CreateNewCollection(model, currentUser);
                _collectionRepository.Create(collection);
                if (model.CustomFields != null) //if user added custom fields
                {
                    var fields = _collectionService.CreateAddFields(model.CustomFields, collection);
                    _collectionRepository.AddFieldsRange(fields);
                }
                await _collectionRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var collection = await _collectionRepository.GetItemAsync(id);
            if (collection == null)
                return NotFound();
            if (!this.HasAccess(collection.User.UserName))
                return Forbid();
            _collectionRepository.Delete(collection);
            if (collection.Image != noPhoto)
                _fileService.DeleteFile(collection.Image);
            await _collectionRepository.SaveChangesAsync();
            return new OkResult();
        }

        [HttpGet]
        public IActionResult Update() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Update(CollectionViewModel model, IFormFile? formFile = null)
        {
            if (ModelState.IsValid)
            {
                var collection = await _collectionRepository.GetItemAsync(model.Id);
                if (collection == null)
                    return NotFound();
                if (!this.HasAccess(collection.User.UserName))
                    return Forbid();
                collection.Description = model.Description;
                collection.Name = model.Name;
                collection.Type = model.Type;

                if (formFile != null) //new image was uploaded otherwise previous image will be remained
                    collection.Image = await _fileService.UpdateFileAsync(formFile, collection.Image);
                _collectionRepository.Update(collection);
                await _collectionRepository.SaveChangesAsync();
                return PartialView("CollectionPage", new List<Collection>() { collection });
            }
            return BadRequest();
        }

        //Personal Page controller of current user
        [HttpPost]
        public async Task<IActionResult> PersonalPage(int id = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            var collections = await _collectionRepository.GetUserItemsAsync(id * itemsCount, itemsCount, user.Id);
            return PartialView("CollectionPage", collections);
        }

        //Calculate Pages for items in pagination
        private static int CountPagesInItems(int collectionCount)
        {
            if (collectionCount % itemsCount == 0)
                return collectionCount / itemsCount;
            return collectionCount / itemsCount + 1;
        }

        //Items of corresponding collection
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CollectionItems(string id)
        {
            var result = await _collectionRepository.GetItemAsync(id);
            if (result == null)
                return NotFound();
            var count = await _collectionService.GetItemCountAsync(id);
            ViewBag.ItemPerPage = CountPagesInItems(count);
            return View(result);
        }

        //Page of collections ordered by Id
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CollectionPage(int id = 0)
        {
            var collections = await _collectionRepository.GetSomeItemsAsync(id * itemsCount, itemsCount);
            return PartialView("CollectionPage", collections);
        }
    }
}
