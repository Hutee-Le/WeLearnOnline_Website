using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly DerekmodeWeLearnSystemContext _context;



        public CourseController(ICourseRepository courseRepository, DerekmodeWeLearnSystemContext context)
        {
            _courseRepository = courseRepository;
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 5; // Số hàng trên mỗi trang
            int pageNumber = page ?? 1; // Trang mặc định
            int totalItems = _courseRepository.GetAllCourses().Count(); // Tổng số mục

            // Đưa các giá trị vào ViewBag
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            var displayedCourses = _courseRepository.GetAllCourses()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.Display=displayedCourses;
            int userId = 1;
            var courses = _courseRepository.GetCoursesWithFavoriteStatus(userId);
            return View("Index", courses);
        }
        public IActionResult Search(string keyword)
        {
            List<Course> course = _courseRepository.Search(keyword);

            return View(course);
        }

        public IActionResult Details(int id)
        {
            var course = _courseRepository.FindCourseByID(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        public IActionResult CourseByCategory(int categoryId)
        {
            var courses = _courseRepository.GetCoursesByCategoryId(categoryId);
            var categoryName = _context.Categories.FirstOrDefault(c => c.CategoriesId == categoryId)?.CategoryName;

            ViewBag.CategoryName = categoryName;

            // Truyền danh sách khóa học đến view
            return View("CourseByCategory", courses);
        }
    }
}
