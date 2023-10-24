using Microsoft.AspNetCore.Mvc;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    public class SkillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
