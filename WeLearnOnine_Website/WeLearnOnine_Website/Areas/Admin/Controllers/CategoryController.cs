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

        public CategoryController(ISkillRepository staffRepository, ICourseRepository courseRepository, ILevelRepository levelRepository, ICategoryRepository categoryRepository)
        {
            _staffRepository = staffRepository;
            _courseRepository = courseRepository;
            _levelRepository = levelRepository;
            _categoryRepository = categoryRepository;
        }

        //View All Table Staff
        public IActionResult Index(int? page)
        {
            int pageSize = 4; // Số lượng mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị

            var paginatedCourses = _categoryRepository.GetRootCategories().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_categoryRepository.GetRootCategories().Count() / pageSize);


            return View("Index", paginatedCourses);
        }
        //public IActionResult Index2(int categoriesId)
        //{
        //    var lst2 = _categoryRepository.GetSecondLevelCategories(categoriesId);
        //    return View("Index2", lst2);
        //}
        // Create 
        [HttpPost]
        public IActionResult SaveCategory(Category category)
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
            return RedirectToAction("Index");
        }

        //public IActionResult CreateCategory()
        //{
        //    var level1 = _categoryRepository.GetRootCategories();
        //    foreach (var catelevel in level1)
        //    {
        //        ViewBag.level1 = new SelectList(level1, "CategoriesId", "CategoryName");
        //        var level2 = _categoryRepository.GetSecondLevelCategories(catelevel);
        //        foreach (var catelevel2 in level2)
        //        {
        //            ViewBag.level2 = new SelectList(level2, "CategoriesId", "CategoryName");
        //            var level3 = _categoryRepository.GetThirdLevelCategories(catelevel2);
        //            foreach (var catelevel3 in level3)
        //            {
        //                ViewBag.level3 = new SelectList(level3, "CategoriesId", "CategoryName");
        //            }
        //        }
        //    }
        //    //// Chọn một CategoriesId từ level1 để truyền vào GetSecondLevelCategories
        //    //int selectedId = (level1.Count > 0) ? level1[0].CategoriesId : 0;

        //    //var level2 = _categoryRepository.GetSecondLevelCategories(selectedId);
        //    //ViewBag.level2 = new SelectList(level2, "CategoriesId", "CategoryName");

        //    //// Chọn một CategoriesId từ level2 để truyền vào GetThirdLevelCategories
        //    //int selectedIdLevel2 = (level2.Count > 0) ? level2[0].CategoriesId : 0;

        //    //var level3 = _categoryRepository.GetThirdLevelCategories(selectedIdLevel2);
        //    //ViewBag.level3 = new SelectList(level3, "CategoriesId", "CategoryName");

        //    return View("CreateCategory", new Category());
        //}


        //public IActionResult CreateCategory()
        //{
        //    var level1 = _categoryRepository.GetRootCategories();
        //    ViewBag.Level1 = new SelectList(level1, "CategoriesId", "CategoryName");

        //    // Default empty SelectList for level 2 and level 3
        //    ViewBag.Level2 = new SelectList(new List<Category>(), "CategoriesId", "CategoryName");
        //    ViewBag.Level3 = new SelectList(new List<Category>(), "CategoriesId", "CategoryName");

        //    return View("CreateCategory", new CategoryViewModel());
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
            _categoryRepository.Update(category);
            if (category.CategoriesId == 0)
            {
                TempData["categoryError"] = "Category not saved!";
            }
            else
            {
                TempData["categorySuccess"] = "Category successfully saved!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditCategory(int id)
        {
            return View("EditCategory", _categoryRepository.GetById(id));
        }

        // Delete
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
