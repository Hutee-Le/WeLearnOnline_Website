using Microsoft.AspNetCore.Mvc;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Controllers
{
    public class MyLearnController : Controller
    {
        private IFavListRepository _favListRepository;
        public MyLearnController(IFavListRepository favListRepository)
        {
            _favListRepository = favListRepository;
        }
        public IActionResult Index()
        {
            return View(_favListRepository.GetAllByUserId(1));
        }
    }
}
