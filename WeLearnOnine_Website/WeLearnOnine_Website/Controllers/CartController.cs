using Microsoft.AspNetCore.Mvc;

namespace WeLearnOnine_Website.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
