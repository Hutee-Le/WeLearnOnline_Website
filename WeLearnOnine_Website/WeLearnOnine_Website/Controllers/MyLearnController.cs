using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
        private IUserCourseRatingRepository _userCourseRatingRepository;
        private readonly Helper _helper;
        public MyLearnController(IFavListRepository favListRepository,
            ICourseRepository courseRepository,
            ICommentRepository commentRepository,
            ILessonRepository lessonRepository,
            IUserCourseRatingRepository userCourseRatingRepository,
            Helper helper)
        {
            _favListRepository = favListRepository;
            _courseRepository = courseRepository;
            _commentRepository = commentRepository;
            _lessonRepository = lessonRepository;
            _userCourseRatingRepository = userCourseRatingRepository;
            _helper = helper;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var claimsPrincipal = HttpContext.User;
            if (User.Identity.IsAuthenticated)
            {
                int userId = await _helper.GetUserId(claimsPrincipal);
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

            // Handle the case where the user is not authenticated, for example, redirect to the login page
            return RedirectToAction("Login", "User");
        }

        public IActionResult MyLearningLessons(int courseid, int? page)
        {
            int pageSize = 5; // Số lượng comment trên mỗi trang
            var course = _courseRepository.FindCourseByID(courseid);
            if (course == null)
            {
                return NotFound();
            }
            List<Comment> comments = _commentRepository.GetById(courseid);
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
        public async Task<IActionResult> PostComment(DetailCourseViewModel model)
        {
            //int UserId = 2;
            var claimsPrincipal = HttpContext.User;
            int UserId = await _helper.GetUserId(claimsPrincipal);
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
                _commentRepository.AddComment(comment);

                return RedirectToAction("MyLearningLessons", new { courseid = CourseId });
            }
            // Validation errors occurred, return errors to the client
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new { success = false, errors = errors });
        }
        //[HttpPost]
        //public IActionResult ReplyToComment(int parentCommentId, string replyContent)
        //{
        //    // Get the parent comment
        //    var parentComment = _commentRepository.GetById(parentCommentId);

        //    // Create a new reply
        //    var reply = new Comment
        //    {
        //        ContentNote = replyContent,
        //        UserId = 2, // Set the user ID accordingly
        //        Date = DateTime.Now,
        //        CourseId = parentComment.CourseId,
        //        ReplyId = parentCommentId
        //    };

        //    // Save the reply to the database
        //    _commentRepository.AddComment(reply);

        //    return RedirectToAction("MyLearningLessons", new { courseid = parentComment.CourseId });
        //}
        public IActionResult ReplyToComment(DetailCourseViewModel model)
        {

            var CourseId = model.CourseId;
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    CourseId = CourseId,
                    //CmtId= model.Comment,
                    StaffId = model.StaffId,
                    ContentNote = model.ContentNote,
                    Date = DateTime.Now,
                };
                _commentRepository.AddComment(comment);

                return RedirectToAction("MyLearningLessons", new { courseid = CourseId });
            }
            // Validation errors occurred, return errors to the client
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new { success = false, errors = errors });
        }
        [HttpPost]
        public IActionResult RateCourse(int courseId, float stars)
        {
            var CourseId = courseId;
            if (ModelState.IsValid)
            {
                var rating = new UserCourseRating
                {
                    CourseId = CourseId,
                    UserId=5,
                    Rating = stars,
                    //Rating = model.ContentNote,
                    // Other rating-related properties
                };

                _userCourseRatingRepository.RatingCourse(rating);
                // You can customize the success message based on your UI framework (e.g., Bootstrap, jQuery UI)
                TempData["SuccessMessage"] = "Thank you for rating the course!";

                return RedirectToAction("MyLearningLessons", new { courseid = CourseId });
            }

            // Validation errors occurred, return errors to the client
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new
            {
                success = false,
                errors = errors
            });

        }
    }
}
