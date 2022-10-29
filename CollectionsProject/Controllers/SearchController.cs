using CollectionsProject.Models.ItemModels;
using CollectionsProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            string requireWord = '+' + searchString;
            ViewBag.Items = await _fullTextSearch.SearchInItems(requireWord);
            ViewBag.Collections = await _fullTextSearch.SearchInCollections(requireWord);
            return PartialView();
        }
    }
}
