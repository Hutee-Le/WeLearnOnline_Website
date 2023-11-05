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
            int pageSize = 4; // Số lượng mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị

            var paginatedCourses = _staffRepository.GetAllStaffs().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_staffRepository.GetAllStaffs().Count() / pageSize);

            return View("Index", paginatedCourses);
        }

        // Detail
        public IActionResult PopupView(string id)
        {
            var model = _staffRepository.FindStaffByID(id);
            return PartialView("_PopupViewPartial", model);
        }

        // Create 
        [HttpPost]
        public async Task<IActionResult> SaveStaff(Staff staff, IFormFile AvatarUrl)
        {
            try {
                if (AvatarUrl != null)
                {
                    staff.AvatarUrl = await _staffRepository.UploadImageAsync(AvatarUrl);
                }

                _staffRepository.Add(staff);

                TempData["staffSuccess"] = "Create staff successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["staffError"] = $"Error create staff: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        public IActionResult CreateStaff()
        {
            return View("CreateStaff", new Staff());
        }

        // Edit
        [HttpPost]
        public async Task<IActionResult> UpdateStaff(Staff staff, IFormFile AvatarUrl)
        {
            try
            {
                if (AvatarUrl != null)
                {
                    staff.AvatarUrl = await _staffRepository.UploadImageAsync(AvatarUrl);
                }

                _staffRepository.Update(staff);

                TempData["staffSuccess"] = "Update Staff successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["staffError"] = $"Error updating staff: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditStaff(string id)
        {
            return View("EditStaff", _staffRepository.FindStaffByID(id));
        }

        // Delete
        public IActionResult Delete(string id)
        {
            try
            {
                _staffRepository.Delete(id);
                TempData["staffSuccess"] = "Delete successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["staffError"] = $"Error delete course: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

    }
}
