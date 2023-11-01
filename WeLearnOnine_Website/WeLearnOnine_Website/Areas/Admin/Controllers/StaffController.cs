using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StaffController : Controller
    {
        private ISkillRepository _staffRepository;

        public StaffController(ISkillRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        //View All Table Staff
        public IActionResult Index(int? page)
        {
            int pageSize = 5; // Số hàng trên mỗi trang
            int pageNumber = page ?? 1; // Trang mặc định
            int totalItems = _staffRepository.GetAllStaffs().Count(); // Tổng số mục

            // Đưa các giá trị vào ViewBag
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            var displayedCourses = _staffRepository.GetAllStaffs()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View("Index", displayedCourses);
        }
    }
}
