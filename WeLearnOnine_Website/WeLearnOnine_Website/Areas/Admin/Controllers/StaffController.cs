using Microsoft.AspNetCore.Mvc;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
