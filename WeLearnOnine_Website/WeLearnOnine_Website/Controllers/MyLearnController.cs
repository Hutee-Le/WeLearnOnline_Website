using Microsoft.AspNetCore.Mvc;

namespace WeLearnOnine_Website.Controllers
{
    public class MyLearnController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
