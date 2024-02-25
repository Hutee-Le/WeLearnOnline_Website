using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Data;
using System.Drawing.Printing;
using System.Text;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.ViewModels;



namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin, Instructor, Training")]

    public class CourseController : Controller
    {
        DerekmodeWeLearnSystemContext ctx;
        private ICourseRepository _courseRepository;
        private ILessonRepository 
            _lessonRepository;
        private ICategoryRepository _categoryRepository;
        private ICategoryCourseRepository _categoryCourseRepository;
        private ILevelRepository _levelRepository;
        private ISkillRepository _staffRepository;


        public CourseController(ICourseRepository courseRepository,
            ICategoryRepository categoryRepository,
            ICategoryCourseRepository categoryCourseRepository,
            ILevelRepository levelRepository,
            ISkillRepository staffRepository,
            DerekmodeWeLearnSystemContext ctx)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _categoryCourseRepository = categoryCourseRepository;
            _levelRepository = levelRepository;
            _staffRepository = staffRepository;
            this.ctx = ctx;
        }

        public IActionResult ShowCourseByCategory(int? CategoryId)
        {
            CategoryId = CategoryId ?? 0;

            var Categories = _categoryRepository.GetAllCategories();
            Categories.Insert(0, new Category { CategoriesId = 0, CategoryName = "---------- Category ----------" });

            ViewBag.CategoryId = new SelectList(Categories, "CategoriesId", "CategoryName", CategoryId);

            var courses = _courseRepository.GetCoursesByCategoryId((int)CategoryId);

            return View("Index", courses);
        }


        public IActionResult Index(int? page, int? CategoryId)
        {
            int pageSize = 4; // Số lượng mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị

            var paginatedCourses = _courseRepository.GetAllCourseWithMany().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_courseRepository.GetAllCourseWithMany().Count() / pageSize);

         

            CategoryId = CategoryId ?? 0;

            var Categories = _categoryRepository.GetAllCategories();

            Categories.Insert(0, new Category { CategoriesId = 0, CategoryName = "---------- Category ----------" });

            ViewBag.CategoryId = new SelectList(Categories, "CategoriesId", "CategoryName", CategoryId);

            return View(paginatedCourses);
        }


        // Detail
       public IActionResult PopupView(int id)
        {
            var model = _courseRepository.GetCategoryViewModelById(id);
            //var categoryNames = _courseRepository.GetCategoriesForCourse(model.CourseId);

            //ViewBag.Categories = categoryNames;

            return PartialView("_PopupViewPartial", model);
        }


        // Create 
        [HttpPost]
        public async Task<IActionResult> SaveCourse(Course course, IFormFile ImageCourseUrl, IFormFile PreviewUrl)
        {
            try
            {
                if (ImageCourseUrl != null)
                {
                    course.ImageCourseUrl = await _courseRepository.UploadImageAsync(ImageCourseUrl);
                }

                if (PreviewUrl != null)
                {
                    course.PreviewUrl = await _courseRepository.UploadVideoAsync(PreviewUrl);
                }

                _courseRepository.Add(course);

                // Lấy danh sách các danh mục đã chọn và cập nhật vào bảng Categories_Course
                if (course.SelectedCategories != null && course.SelectedCategories.Any())
                {
                    foreach (var categoryId in course.SelectedCategories)
                    {
                        var catCourse = new CategoriesCourse
                        {
                            CategoriesId = categoryId,
                            CourseId = course.CourseId // Đảm bảo CourseID đã được tạo khi thêm vào
                        };

                        ctx.CategoriesCourses.Add(catCourse); // _context là đối tượng DbContext, thay thế bằng đối tượng DbContext của bạn
                    }
                    ctx.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                }

                TempData["courseSuccess"] = "Course successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["courseError"] = $"Error saving course: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult CreateCourse()
        {
            var list1 = from c in _levelRepository.GetAllLevels()
                        select new SelectListItem()
                        {
                            Text = c.Name,

                            Value = c.LevelId.ToString(),
                        };
            ViewBag.LevelId = list1.ToList();
            var stafflst = _staffRepository.GetAllStaffs();
            ViewBag.StaffId = new SelectList(stafflst, "StaffId", "StaffName");

            // Lấy danh sách danh mục và chuyển đến view
            var categoryList = _categoryRepository.GetAllCategories(); // Thay thế bằng phương thức lấy danh sách danh mục thực tế
            ViewBag.Categories = new MultiSelectList(categoryList, "CategoriesId", "CategoryName");

            return View("CreateCourse", new Course());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(Course course, IFormFile ImageCourseUrl, IFormFile PreviewUrl, string ExistingImageCourseUrl, string ExistingPreviewUrl)
        {
            try
            {
                if (ImageCourseUrl != null)
                {
                    course.ImageCourseUrl = await _courseRepository.UploadImageAsync(ImageCourseUrl);
                }
                else
                {
                    // Sử dụng giá trị hiện tại nếu không có file mới
                    course.ImageCourseUrl = ExistingImageCourseUrl;
                }

                if (PreviewUrl != null)
                {
                    course.PreviewUrl = await _courseRepository.UploadVideoAsync(PreviewUrl);
                }
                else
                {
                    // Sử dụng giá trị hiện tại nếu không có file mới
                    course.PreviewUrl = ExistingPreviewUrl;
                }

                // Kiểm tra xem danh sách các category đã chọn có rỗng hay không
                if (course.SelectedCategories != null && course.SelectedCategories.Any())
                {
                    // Lấy danh sách các category đã chọn trước đó cho khóa học
                    var selectedCategories = _courseRepository.GetSelectedCategoriesForCourse(course.CourseId);

                    // Xóa các category cũ không được chọn
                    var categoriesToRemove = selectedCategories.Except(course.SelectedCategories);
                    foreach (var categoryId in categoriesToRemove)
                    {
                        // Tìm đối tượng CategoriesCourse trong cơ sở dữ liệu để xóa
                        var catCourseToRemove = ctx.CategoriesCourses
                            .FirstOrDefault(cc => cc.CategoriesId == categoryId && cc.CourseId == course.CourseId);

                        // Xóa đối tượng nếu tìm thấy
                        if (catCourseToRemove != null)
                        {
                            ctx.CategoriesCourses.Remove(catCourseToRemove);
                        }
                    }

                    // Thêm các category mới được chọn
                    var categoriesToAdd = course.SelectedCategories.Except(selectedCategories);
                    foreach (var categoryId in categoriesToAdd)
                    {
                        var catCourseToAdd = new CategoriesCourse
                        {
                            CategoriesId = categoryId,
                            CourseId = course.CourseId
                        };
                        ctx.CategoriesCourses.Add(catCourseToAdd);
                    }
                }
                else
                {
                    // Hành động khi không có category được chọn
                    // Ví dụ: Xóa tất cả các category đã chọn trước đó
                    var selectedCategories = _courseRepository.GetSelectedCategoriesForCourse(course.CourseId);
                    foreach (var categoryId in selectedCategories)
                    {
                        var catCourseToRemove = ctx.CategoriesCourses
                            .FirstOrDefault(cc => cc.CategoriesId == categoryId && cc.CourseId == course.CourseId);

                        if (catCourseToRemove != null)
                        {
                            ctx.CategoriesCourses.Remove(catCourseToRemove);
                        }
                    }
                }

                // Kiểm tra và gán giá trị mặc định cho TimeTotal nếu nó là null
                course.TimeTotal ??= 0;

                // Cập nhật thông tin khóa học
                _courseRepository.Update(course);

                TempData["courseSuccess"] = "Course successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["courseError"] = $"Error updating course: {ex.Message}";
            }

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult EditCourse(int id)
        {
            var levellst = _levelRepository.GetAllLevels();
            var stafflst = _staffRepository.GetAllStaffs();
            ViewBag.LevelId = new SelectList(levellst, "LevelId", "Name");
            ViewBag.StaffId = new SelectList(stafflst, "StaffId", "StaffName");

            // Lấy danh sách danh mục và chuyển đến view
            var categoryList = _categoryRepository.GetAllCategories(); // Thay thế bằng phương thức lấy danh sách danh mục thực tế

            // Lấy danh sách các category đã chọn trước đó cho khóa học có ID là 'id'
            var selectedCategories = _courseRepository.GetSelectedCategoriesForCourse(id);

            // Khởi tạo model Course với các giá trị cần thiết
            var course = _courseRepository.FindCourseByID(id);

            // Gán danh sách các category đã chọn vào thuộc tính SelectedCategories của model
            course.SelectedCategories = selectedCategories;

            ViewBag.Categories = new MultiSelectList(categoryList, "CategoriesId", "CategoryName", course.SelectedCategories);

            return View("EditCourse", course);
        }

        // Delete
        [Authorize(Roles = "Admin, Instructor, Training")]
        public IActionResult Delete(int id)
        {
            try
            {
                _courseRepository.Delete(id);
                TempData["courseSuccess"] = "Delete successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["courseError"] = $"Error delete course: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        // Search
        [Authorize]
        public IActionResult Search(string keyword)
        {
            List<Course> courses;

            if (string.IsNullOrEmpty(keyword))
            {
                courses = ctx.Courses.Include(x => x.Level).Include(x => x.Staff).ToList();
            }
            else
            {
                courses = ctx.Courses.Where(p => p.Title.Contains(keyword) || p.Level.Name.Contains(keyword) || p.Staff.StaffName.Contains(keyword)).Include(x => x.Level).Include(x => x.Staff).ToList();
            }

            if (courses.Count == 0)
            {
                ViewBag.SearchMessage = "Không tìm thấy khóa học.";
            }

            // Chuyển hướng về trang Index với trang hiện tại
            return View("Index", courses);
        }

        [HttpGet]
        public ActionResult ExportCsv()
        {
            // Lấy danh sách các khoá học từ database
            var courses = _courseRepository.GetAllCourses();

            // Tạo một StringBuilder để lưu trữ dữ liệu CSV
            StringBuilder csvContent = new StringBuilder();

            // Thêm tiêu đề cột
            csvContent.AppendLine("CourseID,Title,Level,Staff,Price,Discount Price,Count,Rating,Language,Time Total");

            // Thêm dữ liệu từ danh sách khoá học
            foreach (var course in courses)
            {
                // Kiểm tra null trước khi truy cập các thuộc tính
                string courseId = course.CourseId.ToString() ?? "";
                string title = course.Title ?? "";
                string level = course.Level.Name.ToString();
                string staff = course.Staff.StaffName.ToString();
                string price = course.Price.ToString() ?? "";
                string discountPrice = course.DiscountPrice?.ToString() ?? "";
                string count = course.Count?.ToString() ?? "";
                string rating = course.Rating?.ToString() ?? "";
                string language = course.Language ?? "";
                string timeTotal = course.TimeTotal?.ToString() ?? "";

                language = language.Replace(",", ";");
                csvContent.AppendLine($"{courseId},{title},{level},{staff},{price},{discountPrice},{count},{rating},{language},{timeTotal}");
                // Thêm dữ liệu cho các cột khác tùy thuộc vào các trường bạn muốn xuất
            }

            // Chuyển đổi StringBuilder thành mảng byte và trả về file CSV
            byte[] fileContents = Encoding.UTF8.GetBytes(csvContent.ToString());

            return File(fileContents, "text/csv", "Courses.csv");
        }

    }
}
