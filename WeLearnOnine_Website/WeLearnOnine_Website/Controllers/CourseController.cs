﻿using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly DerekmodeWeLearnSystemContext _context;
        private readonly ICommentRepository _commentRepository;


        public CourseController(ICourseRepository courseRepository, DerekmodeWeLearnSystemContext context, ICommentRepository commentRepository)
        {
            _courseRepository = courseRepository;
            _context = context;
            _commentRepository = commentRepository;
        }

        public IActionResult Index(int? page, int pageSize = 4)
        {
            
            int userId = 4; 
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị)

            List<CourseViewModel> courses;

            if (userId != 0)
            {
                courses = _courseRepository.GetAvailableAndFavoriteCourses(userId);
            }
            else
            {
                // Nếu người dùng chưa đăng nhập, hiển thị danh sách khoá học mặc định
                courses = _courseRepository.GetAllAvailableCourses();
            }

            var paginatedCourses = courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalCourses = courses.Count;
            var totalPages = (int)Math.Ceiling(totalCourses / (double)pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

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

            List<Comment> comments = _commentRepository.GetById(id);
            DetailCourseViewModel model = new()
            {
                CourseId = course.CourseId,
                Course = course,
                Comments = comments,
                StaffId = course.StaffId,
                Title = course.Title,
                Date = DateTime.Now,
                UserId = 2
            };
            ViewBag.Comments = comments;
            return View(model);
        }
        [HttpPost]
        public IActionResult PostComment(DetailCourseViewModel model)
        {
            int UserId = 2;
            var Date = DateTime.Now;
            var CourseId = model.CourseId;
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    CourseId = model.CourseId,
                    UserId = UserId,
                    ContentNote = model.ContentNote,
                    Date = Date,
                };

                // Lưu comment vào cơ sở dữ liệu (sử dụng repository hoặc Entity Framework)
                _commentRepository.AddComment(comment);
                //var commentPartial = PartialView("PostComment", comment);
                // Tạo một partial view chứa comment
                //return View(model);
                //return View(model);
                //var commentPartial = PartialView("PostComment", comment);
                // Trả về partial view để hiển thị comment mà không cần chuyển trang
                //return Json(new { success = true, comment = commentPartial });
                return RedirectToAction("Details",  new { id = CourseId } );
            }

            // Validation errors occurred, return errors to the client
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new { success = false, errors = errors });
            
        }

        public IActionResult CourseByCategory(int categoryId)
        {
            var courses = _courseRepository.GetCoursesByCategoryId(categoryId);
            var categoryName = _context.Categories.FirstOrDefault(c => c.CategoriesId == categoryId)?.CategoryName;

            ViewBag.CategoryName = categoryName;

            // Truyền danh sách khóa học đến view
            return View("CourseByCategory", courses);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int courseId)
        {
            int userId = 2; // Lấy ID người dùng từ session hoặc cookie
            if (userId == 0)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thực hiện chức năng này." });
            }
            bool result = await _courseRepository.AddToFavorites(userId, courseId);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int courseId)
        {
            int userId = 2 ; // Lấy ID người dùng từ session hoặc cookie
            if (userId == 0)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thực hiện chức năng này." });
            }
            bool result = await _courseRepository.RemoveFromFavorites(userId, courseId);
            return Json(new { success = result });
        }

    }
}
