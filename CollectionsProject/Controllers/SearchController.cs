using CollectionsProject.Models.ItemModels;
using CollectionsProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly IFullTextSearch _fullTextSearch;

        public SearchController(IFullTextSearch fullTextSearch)
        {
            _fullTextSearch = fullTextSearch;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return Ok();
            ViewBag.Items = await _fullTextSearch.SearchInItems(searchString);
            ViewBag.Collections = await _fullTextSearch.SearchInCollections(searchString);
            return PartialView();
        }
    }
}
