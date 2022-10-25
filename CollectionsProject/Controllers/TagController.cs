using CollectionsProject.Repositories.Interfaces;
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
        public IActionResult ItemList(string id) => View("ItemList", id);
    }
}
