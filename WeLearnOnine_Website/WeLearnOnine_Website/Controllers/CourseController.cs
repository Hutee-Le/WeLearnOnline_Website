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

        public IActionResult Index()
        {
            //var courses = _courseRepository.GetAllCourses();

            //// Lặp qua danh sách khóa học và lấy Name của Staff thông qua StaffId
            //foreach (var course in courses)
            //{
            //    if (course.StaffId != null)
            //    {
            //        var staff = _context.Staff.FirstOrDefault(s => s.StaffId == course.StaffId);
            //        if (staff != null)
            //        {
            //            course.StaffId = staff.StaffName;
            //        }
            //    }

            //    // Lấy Name của Level thông qua LevelId (giống như trước)
            //    if (course.LevelId != null)
            //    {
            //        var level = _context.Levels.FirstOrDefault(l => l.LevelId == course.LevelId);
            //        if (level != null)
            //        {
            //            course.Level.Name = level.Name;
            //        }
            //    }
            //}

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
