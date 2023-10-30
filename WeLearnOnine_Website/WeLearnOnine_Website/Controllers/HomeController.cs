using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFavListRepository _favListRepository;
        private readonly ICourseRepository _courseRepository;
        public HomeController(ILogger<HomeController> logger, IFavListRepository favListRepository, ICourseRepository courseRepository)
        {
            _logger = logger;
            _favListRepository = favListRepository;
            _courseRepository = courseRepository;
        }

        public IActionResult Index()
        {
            List<Course> courses = _courseRepository.GetAllCourses();
            return View(courses);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FavList()
        {
            return View(_favListRepository.GetAllByUserId(1));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}