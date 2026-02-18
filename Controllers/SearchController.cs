using MESSAGENOID.Interfaces;
using MESSAGENOID.Models;
using MESSAGENOID.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MESSAGENOID.Controllers
{
    public class SearchController : Controller
    {

        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> searchResults(string userName)
        {
            var results = await _searchService.SearchUsersAsync(userName,20);
            ViewBag.Query = userName;
            return View(results);
        }

    }
}
