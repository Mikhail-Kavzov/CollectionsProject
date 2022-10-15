using CollectionsProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CollectionsProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}