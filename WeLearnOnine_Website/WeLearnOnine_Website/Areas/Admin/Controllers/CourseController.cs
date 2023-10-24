using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;


namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private ICourseRepository _courseRepository;
        private ILessonRepository _lessonRepository;
        private ICategoryRepository _categoryRepository;
        private ILevelRepository _levelRepository;
        private IStaffRepository _staffRepository;

        public CourseController(ICourseRepository courseRepository, ICategoryRepository categoryRepository, ILevelRepository levelRepository, IStaffRepository staffRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _levelRepository = levelRepository;
            _staffRepository = staffRepository;
        }

        //View All Table Course
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

            return View("Index", displayedCourses);
        }

        // Create 
        [HttpPost]
        public IActionResult SaveCourse(Course course) 
        {
            _courseRepository.Add(course);
            return RedirectToAction("Index");
        }

        public IActionResult CreateCourse()
        {
            var list1 = from c in _levelRepository.GetAllLevels()
                        select new SelectListItem()
                        {
                            Text = c.Name,
                            
                            Value = c.LevelId.ToString(),
                        };
            ViewBag.LevelId = list1.ToList();
            return View("CreateCourse", new Course());
        }

        [HttpPost]
        // Edit
        public IActionResult UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
            return RedirectToAction("Index");
        }

        public IActionResult EditCourse(int id)
        {
            //var list1 = from c in _levelRepository.GetAllLevels()
            //            select new SelectListItem()
            //            {
            //                Text = c.Name,
            //                Value = c.LevelId.ToString(),
            //            };
            //ViewBag.LevelId = list1.ToList();

            var levellst = _levelRepository.GetAllLevels();
            ViewBag.LevelId = new SelectList(levellst, "LevelId", "Name");
            return View("EditCourse", _courseRepository.FindCourseByID(id));
        }
    
        // Delete
        public IActionResult Delete(int id) 
        { 
            _courseRepository.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
