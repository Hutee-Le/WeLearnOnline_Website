using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin, Instructor, Training")]
    public class LessonController : Controller
    {
        private ICourseRepository _courseRepository;
        private ILessonRepository _lessonRepository;
        private ITypeRepository _typeRepository;
        private readonly IConfiguration _configuration;
        DerekmodeWeLearnSystemContext ctx;

      

        public LessonController(ILessonRepository lessonRepository, 
            ICourseRepository courseRepository, 
            IConfiguration configuration, 
            ITypeRepository typeRepository, 
            DerekmodeWeLearnSystemContext ctx)
        {
            _courseRepository = courseRepository;
            _lessonRepository = lessonRepository;
            _configuration = configuration;
            _typeRepository = typeRepository;
            this.ctx = ctx;
        }

        //View All Table Staff
        public IActionResult Index(int? page, int? CourseId)
        {
            int pageSize = 4; // Số lượng mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị

            var paginatedCourses = _lessonRepository.GetAllBaiHoc().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            CourseId = CourseId ?? 0;

            var Courses = ctx.Courses.ToList();

            Courses.Insert(0, new Course { CourseId = 0, Title = "---------- Courses ----------" });

            ViewBag.CourseId = new SelectList(Courses, "CourseId", "Title", CourseId);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_lessonRepository.GetAllBaiHoc().Count() / pageSize);

            return View(paginatedCourses);
        }


    public IActionResult ShowLessonByCourse(int? page, int? CourseId)
    {
        int pageSize = 4; // Số lượng mục trên mỗi trang
        int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị
        CourseId = CourseId ?? 0;

        var Courses = ctx.Courses.ToList();
        Courses.Insert(0, new Course { CourseId = 0, Title = "---------- Courses ----------" });

        ViewBag.CourseId = new SelectList(Courses, "CourseId", "Title", CourseId);

        // Lấy danh sách bài học theo CourseId và thực hiện phân trang
        var lessons = ctx.Lessons
            .Where(x => x.CourseId == CourseId)
            .Include(c => c.Type)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = pageNumber;
        ViewBag.TotalPages = (int)Math.Ceiling((double)ctx.Lessons.Where(x => x.CourseId == CourseId).Count() / pageSize);

        return View("Index", lessons);
    }


    // Detail
    public IActionResult PopupView(int id)
        {
            var mdl = _lessonRepository.GetById(id);
            return PartialView("_LessonDetailsViewPartial", mdl);
        }

        // Create 
        [HttpPost]
        public async Task<IActionResult> SaveLesson(Lesson lesson, IFormFile ImageLessonUrl, IFormFile UrlVideo)
        {
            try
            {
                if (ImageLessonUrl != null)
                {
                    lesson.ImageLessonUrl = await _lessonRepository.UploadImageAsync(ImageLessonUrl);
                }

                if (UrlVideo != null)
                {
                    lesson.UrlVideo= await _lessonRepository.UploadVideoAsync(UrlVideo);
                }

                _lessonRepository.Add(lesson);

                TempData["lessonSuccess"] = "Lesson successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["lessonError"] = $"Error saving lesson: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public IActionResult CreateLesson()
        {
            var courselst = _courseRepository.GetAllCourses();
            ViewBag.CourseId = new SelectList(courselst, "CourseId", "Title");

            var typesList = _typeRepository.GetAll();
            ViewBag.TypeId = new SelectList(typesList, "TypeId", "Title");

            return View("CreateLesson", new Lesson());
        }

        // Edit
        [HttpPost]
        public async Task<IActionResult> UpdateLesson(Lesson lesson, IFormFile ImageLessonUrl, IFormFile UrlVideo, string ExistingImageLessonUrl, string ExistingUrlVideo)
        {
            try
            {
                if (ImageLessonUrl != null)
                {
                    lesson.ImageLessonUrl = await _lessonRepository.UploadImageAsync(ImageLessonUrl);
                }
                else
                {
                    // Sử dụng giá trị hiện tại nếu không có file mới
                    lesson.ImageLessonUrl = ExistingImageLessonUrl;
                }

                if (UrlVideo != null)
                {
                    lesson.UrlVideo = await _lessonRepository.UploadVideoAsync(UrlVideo);
                }
                else
                {
                    // Sử dụng giá trị hiện tại nếu không có file mới
                    lesson.UrlVideo = ExistingUrlVideo;
                }

                _lessonRepository.Update(lesson);

                TempData["lessonSuccess"] = "Lesson successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["lessonError"] = $"Error updating lesson: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditLesson(int id)
        {
            var courselst = _courseRepository.GetAllCourses();
            ViewBag.CourseId = new SelectList(courselst, "CourseId", "Title");

            var typesList = _typeRepository.GetAll();
            ViewBag.TypeId = new SelectList(typesList, "TypeId", "Title");
            return View("EditLesson", _lessonRepository.GetById(id));
        }

        // Delete
        public IActionResult Delete(int id)
        {
            try
            {
                _lessonRepository.Delete(id);
                TempData["lessonSuccess"] = "Delete successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["lessonError"] = $"Error delete lesson: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        // Search
        public IActionResult Search(string keyword)
        {
            List<Lesson> lessonsItem;

            if (string.IsNullOrEmpty(keyword))
            {
                lessonsItem = ctx.Lessons.Include(x => x.Course).Include(x => x.Type).ToList();
            }
            else
            {
                lessonsItem = ctx.Lessons.Where(p => p.Title.Contains(keyword) || p.Type.Title.Contains(keyword)|| p.Course.Title.Contains(keyword)).Include(x => x.Course).Include(x => x.Type).ToList();
            }

            if (lessonsItem.Count == 0)
            {
                ViewBag.SearchMessage = "Không tìm thấy bài học nào.";
            }

            // Chuyển hướng về trang Index với trang hiện tại
            return View("Index", lessonsItem);
        }

        [HttpGet]
        public ActionResult ExportCsv()
        {
            // Lấy danh sách các bài học từ database
            var lessons = _lessonRepository.GetAllBaiHoc();

            // Tạo một StringBuilder để lưu trữ dữ liệu CSV
            StringBuilder csvContent = new StringBuilder();

            // Thêm tiêu đề cột
            csvContent.AppendLine("LessonID,Title,Course,Type,PosInList,Time");

            // Thêm dữ liệu từ danh sách bài học
            foreach (var lesson in lessons)
            {
                // Kiểm tra null trước khi truy cập các thuộc tính
                string courseId = lesson.LessonId.ToString() ?? "";
                string title = EscapeCsvField(lesson.Title);
                string course = EscapeCsvField(lesson.Course.Title.ToString());
                string type = EscapeCsvField(lesson.Type.Title.ToString());
                string stt = lesson.Stt?.ToString() ?? "";
                string time = lesson.Time?.ToString() ?? "";

                csvContent.AppendLine($"{courseId},{title},{course},{type},{stt},{time}");
                // Thêm dữ liệu cho các cột khác tùy thuộc vào các trường bạn muốn xuất
            }

            // Chuyển đổi StringBuilder thành mảng byte và trả về file CSV
            byte[] fileContents = Encoding.UTF8.GetBytes(csvContent.ToString());

            return File(fileContents, "text/csv", "Lesson.csv");
        }

        private string EscapeCsvField(string field)
        {
            // Hàm này đặt giá trị trong dấu ngoặc kép nếu nó chứa dấu phẩy, ký tự đặc biệt, hoặc dấu cách
            if (field.Contains(",") || field.Contains("\"") || field.Contains(" ") || field.Contains("'"))
            {
                return $"\"{field}\"";
            }
            return field;
        }


    }
}
