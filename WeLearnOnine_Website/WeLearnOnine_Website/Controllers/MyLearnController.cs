using Microsoft.AspNetCore.Mvc;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Controllers
{
    public class MyLearnController : Controller
    {
        private IFavListRepository _favListRepository;
        private ICourseRepository _courseRepository;
        public MyLearnController(IFavListRepository favListRepository, ICourseRepository courseRepository)
        {
            _favListRepository = favListRepository;
            _courseRepository = courseRepository;
        }
        public IActionResult Index()
        {
            return View(_courseRepository.MyCourses(1));
        }

        public IActionResult WishList()
        {
            return View(_favListRepository.GetAllByUserId(1));
        }

        public IActionResult MyCourses()
        {
            return View(_courseRepository.MyCourses(1));
        }
    }
}
