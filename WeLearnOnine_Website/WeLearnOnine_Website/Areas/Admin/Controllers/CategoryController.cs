using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
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


        //public IActionResult CreateCourse()
        //{
        //    var levelTop = from c in _categoryRepository.GetRootCategories()
        //                select new SelectListItem()
        //                {
        //                    Text = c.CategoryName,

        //                    Value = c.CategoriesId.ToString(),
        //                };
        //    ViewBag.LevelId = levelTop.ToList();
        //    return View("CreateCourse", new Category());
        //}


        //[HttpPost]
        //public IActionResult CreateCategory(Category model)
        //{
        //    // Your create category logic here...

        //    return RedirectToAction("Index"); // Redirect to the appropriate action after creating the category
        //}

        //[HttpGet]
        //public IActionResult GetLevel2Categories(int id)
        //{
        //    var level2 = _categoryRepository.GetSecondLevelCategories(id);
        //    return Json(level2.Select(c => new { c.CategoriesId, c.CategoryName }));
        //}

        //[HttpGet]
        //public IActionResult GetLevel3Categories(int id)
        //{
        //    var level3 = _categoryRepository.GetThirdLevelCategories(id);
        //    return Json(level3.Select(c => new { c.CategoriesId, c.CategoryName }));
        //}


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
    }
}
