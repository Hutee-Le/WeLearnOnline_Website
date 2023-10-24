using Microsoft.AspNetCore.Mvc;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
