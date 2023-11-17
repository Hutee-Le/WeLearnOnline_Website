using Microsoft.AspNetCore.Mvc;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Controllers
{
    public class MyLearnController : Controller
    {
        private IFavListRepository _favListRepository;
        private ICourseRepository _courseRepository;
        private readonly ICommentRepository _commentRepository;
        private ILessonRepository _lessonRepository;
        public MyLearnController(IFavListRepository favListRepository, ICourseRepository courseRepository, ICommentRepository commentRepository, ILessonRepository lessonRepository)
        {
            _favListRepository = favListRepository;
            _courseRepository = courseRepository;
            _commentRepository = commentRepository;
            _lessonRepository = lessonRepository;
        }
        public IActionResult Index(int? page)
        {
            int userId = 2;
            int pageSize = 2; // Số lượng mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị)

            List<Bill> MyCourses = _courseRepository.MyCourses(userId);
            int totalCourses = MyCourses.SelectMany(b => b.BillDetails.Select(d => d.Course)).Count();

            var paginatedDetails = MyCourses
                .SelectMany(b => b.BillDetails)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCourses / pageSize);

            var viewModel = new MyLearnViewModel
            {
                MyCourses = MyCourses, // Assuming MyCourses is a list of Bill objects
                PaginatedDetails = paginatedDetails, // Add this property to your view model
                WishList = _favListRepository.GetAllByUserId(userId),
            };

            return View(viewModel);
        }
        public IActionResult MyLearningLessons(int courseid, int? page)
        {
            int pageSize = 5; // Số lượng comment trên mỗi trang
            var course = _courseRepository.FindCourseByID(courseid);
            if (course == null)
            {
                return NotFound();
            }
            //List<Lesson> lessons = _lessonRepository.GetLessonById(courseid, lessonid);

            List<Comment> comments = _commentRepository.GetById(courseid);
            // var recentcomments = _commentRepository.GetRecentComments(courseid);
            //var allComments = _commentRepository.GetCommentsByCourseId(courseid);

            // Phân trang
            var pageNumber = page ?? 1;
            //var commentsForPage = allComments.ToPagedList(pageNumber, pageSize);
            var paginatedCourses = comments.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)comments.Count / pageSize);
            DetailCourseViewModel model = new()
            {
                CourseId = course.CourseId,
                Course = course,
                // Lesson = lessons,
                Comments = paginatedCourses,
                //StaffId = course.StaffId,
                //Title = course.Title,
                UserId = 2
            };

            ViewBag.Courseid = course.CourseId;

            return View(model);
        }
        [HttpPost]
        public IActionResult PostComment(DetailCourseViewModel model)
        {
            int UserId = 2;
            var CourseId = model.CourseId;
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    CourseId = model.CourseId,
                    UserId = UserId,
                    ContentNote = model.ContentNote,
                    Date = DateTime.Now,
                };

                // Lưu comment vào cơ sở dữ liệu (sử dụng repository hoặc Entity Framework)
                _commentRepository.AddComment(comment);
                //var commentPartial = PartialView("PostComment", comment);
                // Tạo một partial view chứa comment
                //return View(model);
                //var commentPartial = PartialView("PostComment", comment);
                // Trả về partial view để hiển thị comment mà không cần chuyển trang
                //return Json(new { success = true, comment = commentPartial });
                return RedirectToAction("MyLearningLessons", new { courseid = CourseId });
            }
            // Validation errors occurred, return errors to the client
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new { success = false, errors = errors });
        }
        public IActionResult GetVideo(int courseId, int lessonid)
        {
            var course = _courseRepository.FindCourseByID(courseId);
            Lesson lesson = _lessonRepository.getLessonbyCourse(courseId, lessonid);
            List<Comment> comments = _commentRepository.GetById(courseId);

            DetailCourseViewModel model = new()
            {
                CourseId = course.CourseId,
                Course = course,
                StaffId = course.StaffId,
                Comments = comments,
                Currentlesson = lesson,
                LessonId = lesson.LessonId,
                ContentNote = "ContentNote",
                UserName = "Linh",
                Title = course.Title,
                Date = DateTime.Now,
                UserId = 2
            };
            // var moreComments = _commentRepository.GetMoreComments(courseId,les);
            return View(model);
        }

    }
}
