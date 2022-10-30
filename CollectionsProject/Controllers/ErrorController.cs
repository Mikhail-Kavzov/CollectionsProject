using Microsoft.AspNetCore.Mvc;

namespace CollectionsProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult PageNotFound() => View();
    }
}
