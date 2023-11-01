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
            
            int userId = 0; // Hoặc lấy userId từ đâu đó nếu cần
            int pageSize = 4; // Số lượng mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị)

            List<CourseViewModel> courses;

            if (userId != 0)
            {
                courses = _courseRepository.GetAllCoursesWithDetails(userId);
            }
            else
            {
                // Nếu người dùng chưa đăng nhập, hiển thị danh sách khoá học mặc định
                courses = _courseRepository.GetAllCoursesWithDetails();
            }

            var paginatedCourses = courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)courses.Count() / pageSize);

            return View("Index", paginatedCourses);
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
