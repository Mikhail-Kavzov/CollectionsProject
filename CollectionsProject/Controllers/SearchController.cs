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

        //full text search action
        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString) || searchString.Length<3) //search limit (ex MySQL won't find text less than 3 symbols)
                return BadRequest();
            string requireWord = '+' + searchString; // '+' means require word (in MySql in Boolean mode)
            ViewBag.Items = await _fullTextSearch.SearchInItems(requireWord);
            ViewBag.Collections = await _fullTextSearch.SearchInCollections(requireWord);
            return PartialView();
        }
    }
}
