using CollectionsProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    public class TagController
    {
        private readonly ITagRepository _tagRepository;

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
    }
}
