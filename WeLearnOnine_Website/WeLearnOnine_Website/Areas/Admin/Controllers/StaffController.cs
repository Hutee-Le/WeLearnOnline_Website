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

        // Create 
        [HttpPost]
        public IActionResult SaveStaff(Staff staff)
        {
            _staffRepository.Add(staff);
            if (staff.StaffId == " ")
            {
                TempData["staffError"] = "Course not saved!";
            }
            else
            {
                TempData["staffSuccess"] = "Course successfully saved!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult CreateStaff()
        {
            return View("CreateStaff", new Staff());
        }

        [HttpPost]
        // Edit
        public IActionResult UpdateStaff(Staff staff)
        {
            _staffRepository.Update(staff);
            if (staff.StaffId == " ")
            {
                TempData["staffError"] = "Staff not saved!";
            }
            else
            {
                TempData["staffSuccess"] = "Staff successfully saved!";
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
            _staffRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
