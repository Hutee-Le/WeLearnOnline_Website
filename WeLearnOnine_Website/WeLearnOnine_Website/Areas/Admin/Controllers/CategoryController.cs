using Microsoft.AspNetCore.Mvc;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        
        public IActionResult Index(int? page)
        {

            int pageSize = 5; // Số hàng trên mỗi trang
            int pageNumber = page ?? 1; // Trang mặc định
            int totalItems = _categoryRepository.GetAllCategories().Count(); // Tổng số mục

            // Đưa các giá trị vào ViewBag
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            var displayedCategoties = _categoryRepository.GetAllCategories()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View("Index", displayedCategoties);
        }
    }
}
