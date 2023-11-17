using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.Services;

namespace WeLearnOnine_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFavListRepository _favListRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEmailService _emailService;
        public HomeController(ILogger<HomeController> logger, IFavListRepository favListRepository, ICourseRepository courseRepository, IEmailService emailService)
        {
            _logger = logger;
            _favListRepository = favListRepository;
            _courseRepository = courseRepository;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var courses = _courseRepository.GetTopCourses(4);
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

        public IActionResult SendMail()
        {
            var toAddresses = new List<string> { "lenhut0929@gmail.com", "lenhut2909@gmail.com" };

            // Gửi email đến nhiều địa chỉ
            _emailService.SendEmailAsync(toAddresses, "Sending test email for Project Welearn", "Đây là mail test bạn có thể bỏ qua mail này.");

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}