using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Text;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ISkillRepository _staffRepository;
        private ICourseRepository _courseRepository;
        private ILevelRepository _levelRepository;
        private ICategoryRepository _categoryRepository;
        DerekmodeWeLearnSystemContext ctx;

        public CategoryController(ISkillRepository staffRepository, 
            ICourseRepository courseRepository, 
            ILevelRepository levelRepository, 
            ICategoryRepository categoryRepository,
            DerekmodeWeLearnSystemContext ctx)
        {
            _staffRepository = staffRepository;
            _courseRepository = courseRepository;
            _levelRepository = levelRepository;
            _categoryRepository = categoryRepository;
            this.ctx = ctx;
        }

        //View All Table Staff
        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var paginatedCourses = _categoryRepository.GetAllCategories().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_categoryRepository.GetAllCategories().Count() / pageSize);


            return View("Index", paginatedCourses);
        }
        //public IActionResult Index()
        //{
        //    var categories = _categoryRepository.GetAllCategories();

        //    return View("Index", categories);
        //}

        // Create 
        [HttpPost]
        public IActionResult SaveCategory(Category category)
        {
            var parentID = category.ParentCategories;
            // Kiểm tra xem CategoryName có giá trị hay không
            if (!string.IsNullOrEmpty(category.CategoryName))
            {
                _categoryRepository.Create(category);

                if (category.CategoriesId == 0)
                {
                    TempData["categoryError"] = "Category not saved!";
                }
                else
                {
                    TempData["categorySuccess"] = "Category successfully saved!";
                }
            }
            else
            {
                TempData["categoryError"] = "CategoryName cannot be null or empty!";
            }

            return RedirectToAction("Index");
        }

        public IActionResult CreateCategory()
        {
            var catelst = _categoryRepository.GetAllCategories();
            ViewBag.CategoriesId = new SelectList(catelst, "CategoriesId", "CategoryName");
            return View("CreateCategory", new Category());
        }


        [HttpPost]
        // Edit
        public IActionResult UpdateCategory(Category category)
        {
            var parentID = category.ParentCategories;
            // Kiểm tra xem CategoryName có giá trị hay không
            if (!string.IsNullOrEmpty(category.CategoryName))
            {
                _categoryRepository.Update(category);

                if (category.CategoriesId == 0)
                {
                    TempData["categoryError"] = "Category not saved!";
                }
                else
                {
                    TempData["categorySuccess"] = "Category successfully saved!";
                }
            }
            else
            {
                TempData["categoryError"] = "CategoryName cannot be null or empty!";
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditCategory(int id)
        {
            var catelst = _categoryRepository.GetAllCategories();
            ViewBag.CategoriesId = new SelectList(catelst, "CategoriesId", "CategoryName");
            return View("EditCategory", _categoryRepository.GetById(id));
        }

        // Delete
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }


        public IActionResult PopupView(int id)
        {
            var model = _categoryRepository.GetById(id);
            //var viewModel = new MyLearnViewModel
            //{
            //    CategoryId = model,
            //    SubCategories = _categoryRepository.GetSubCategories(id)
            //};
            return PartialView("_CategoryPopupViewPartial", model);
        }

        public IActionResult Search(string keyword)
        {
            List<Category> categories;

            if (string.IsNullOrEmpty(keyword))
            {
                categories = ctx.Categories.ToList();
            }
            else
            {
                categories = ctx.Categories.Where(p => p.CategoryName.Contains(keyword)).ToList();
            }

            if (categories.Count == 0)
            {
                ViewBag.SearchMessage = "Không tìm thấy thể loại này.";
            }

            // Chuyển hướng về trang Index với trang hiện tại
            return View("Index", categories);
        }

        [HttpGet]
        public ActionResult ExportCsv()
        {
            // Lấy danh sách các bài học từ database
            var categories = _categoryRepository.GetAllCategories();

            // Tạo một StringBuilder để lưu trữ dữ liệu CSV
            StringBuilder csvContent = new StringBuilder();

            // Thêm tiêu đề cột
            csvContent.AppendLine("CategoriesId,CategoryName,ParentCategories");

            // Thêm dữ liệu từ danh sách bài học
            foreach (var category in categories)
            {
                // Kiểm tra null trước khi truy cập các thuộc tính
                string categoriesId = category.CategoriesId.ToString() ?? "";
                string categoryName = EscapeCsvField(category.CategoryName.ToString());
                string parentCategories = EscapeCsvField(category.ParentCategories?.ToString() ?? "Default");

                csvContent.AppendLine($"{categoriesId},{categoryName},{parentCategories}");
                // Thêm dữ liệu cho các cột khác tùy thuộc vào các trường bạn muốn xuất
            }

            // Chuyển đổi StringBuilder thành mảng byte và trả về file CSV
            byte[] fileContents = Encoding.UTF8.GetBytes(csvContent.ToString());

            return File(fileContents, "text/csv", "Category.csv");
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
