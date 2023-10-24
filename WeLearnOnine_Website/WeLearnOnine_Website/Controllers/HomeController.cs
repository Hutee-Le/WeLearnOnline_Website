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
		public HomeController(ILogger<HomeController> logger, IFavListRepository favListRepository)
		{
			_logger = logger;
			_favListRepository = favListRepository;
		}

		public IActionResult Index()
		{
			return View();
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