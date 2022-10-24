using CollectionsProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    public class TagController:Controller
    {
        private readonly ITagRepository _tagRepository;
        private const int itemCount= 5;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository=tagRepository;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagRepository.GetTagNamesAsync();
            return new JsonResult(tags);
        }

        [HttpPost]
        public async Task<IActionResult> ItemPage(string tagName,int Page = 0)
        {
            var items = await _tagRepository.GetTagItems(tagName, Page * itemCount, itemCount);
            return PartialView("~/Views/Item/ItemShortDescription.cshtml",items);
        }

        [HttpGet]
        public IActionResult ItemPage(string id) => View(id);
    }
}
