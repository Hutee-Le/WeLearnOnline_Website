using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        private ISkillRepository _staffRepository;
        DerekmodeWeLearnSystemContext ctx;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StaffController(ISkillRepository staffRepository,
            DerekmodeWeLearnSystemContext ctx,
            RoleManager<IdentityRole> roleManager)
        {
            _staffRepository = staffRepository;
            this.ctx = ctx;
            _roleManager = roleManager;
        }

        //View All Table Staff
        //[Authorize]
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
        //[Authorize]
        public IActionResult CreateStaff()
        {
            // Lấy danh sách tên các vai trò từ bảng AspNetRole
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();

            // Truyền danh sách vai trò vào SelectList
            ViewBag.Rolelst = new SelectList(roles);

            return View("CreateStaff", new Staff());
        }


        // Edit
        [HttpPost]
        public async Task<IActionResult> UpdateStaff(Staff staff, IFormFile ImageAvartarFile, string ExistingImageAvartarFile)
        {
            try
            {
                if (ImageAvartarFile != null)
                {
                    staff.AvatarUrl = await _staffRepository.UploadImageAsync(ImageAvartarFile);
                }
                else
                {
                    // Sử dụng giá trị hiện tại nếu không có file mới
                    staff.AvatarUrl = ExistingImageAvartarFile;
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
            //ViewBag.Rolelst = new SelectList(new List<string>() { "Admin", "Instructor", "Đào Tạo" });
            // Lấy danh sách tên các vai trò từ bảng AspNetRole
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();

            // Truyền danh sách vai trò vào SelectList
            ViewBag.Rolelst = new SelectList(roles);
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
                TempData["staffError"] = $"Error delete Staff: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        // Search
        public IActionResult Search(string keyword)
        {
            List<Staff> staffs;

            if (string.IsNullOrEmpty(keyword))
            {
                staffs = ctx.Staff.ToList();
            }
            else
            {
                staffs = ctx.Staff
            .Where(p =>
                p.StaffName.Contains(keyword) ||
                p.PhoneNumber.Contains(keyword) ||
                p.Email.Contains(keyword) ||
                p.Desciption.Contains(keyword)
            ).ToList();

            }

            if (staffs.Count == 0)
            {
                ViewBag.SearchMessage = "Không tìm thấy Staff nào!";
            }

            // Chuyển hướng về trang Index với trang hiện tại
            return View("Index", staffs);
        }
        [HttpGet]
        public ActionResult ExportCsv()
        {
            // Lấy danh sách các bài học từ database
            var staffs = _staffRepository.GetAllStaffs();

            // Tạo một StringBuilder để lưu trữ dữ liệu CSV
            StringBuilder csvContent = new StringBuilder();

            // Thêm tiêu đề cột
            csvContent.AppendLine("StaffId,StaffName,Desciption,Experience,Role,PhoneNumber,Email,Password,Rating,Address");

            // Thêm dữ liệu từ danh sách bài học
            foreach (var staff in staffs)
            {
                // Kiểm tra null trước khi truy cập các thuộc tính
                string staffId = staff.StaffId.ToString() ?? "";
                string staffName = EscapeCsvField(staff.StaffName?.ToString() ?? "");
                string desciption = EscapeCsvField(staff.Desciption?.ToString() ?? "");
                string experience = EscapeCsvField(staff.Experience?.ToString() ?? "");
                string role = staff.Role?.ToString() ?? "";
                string phoneNumber = staff.PhoneNumber?.ToString() ?? "";
                string email = staff.Email?.ToString() ?? "";
                string password = staff.Password?.ToString() ?? "";
                string rating = staff.Rating?.ToString() ?? "";
                string address = staff.Address?.ToString() ?? "";

                csvContent.AppendLine($"{staffId},{staffName},{desciption},{experience},{role},{phoneNumber},{email},{password},{rating},{address}");
                // Thêm dữ liệu cho các cột khác tùy thuộc vào các trường bạn muốn xuất
            }

            // Chuyển đổi StringBuilder thành mảng byte và trả về file CSV
            byte[] fileContents = Encoding.UTF8.GetBytes(csvContent.ToString());

            return File(fileContents, "text/csv", "Staff.csv");
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
