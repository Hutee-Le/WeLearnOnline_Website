using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

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
        public IActionResult Index2(int categoriesId)
        {
            var lst2 = _categoryRepository.GetSubCategories(categoriesId);
            return View("Index2", lst2);
        }
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

        public IActionResult CreateCategory()
        {
            return View("CreateCategory", new Category());
        }

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
