using CollectionsProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{

    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private const int itemCount = 15;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagRepository.GetTagNamesAsync();
            return new JsonResult(tags);
        }

        [HttpPost]
        public async Task<IActionResult> ItemPage(string TagName, int Page = 0)
        {
            var items = await _tagRepository.GetTagItems(TagName, Page * itemCount, itemCount);
            return PartialView("~/Views/Item/ItemShortDescription.cshtml", items);
        }

        [HttpGet]
        public async Task<IActionResult> ItemList(string id)
        {
            var count = await _tagRepository.CountTagInItemsAsync(id);
            if (count == 0)
                return NotFound();
            return View("ItemList", id);
        }
    }
}
