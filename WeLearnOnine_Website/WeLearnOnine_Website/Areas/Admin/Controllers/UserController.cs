using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        DerekmodeWeLearnSystemContext ctx;

        public UserController(IUserRepository userRepository, DerekmodeWeLearnSystemContext ctx)
        {
            _userRepository = userRepository;
            this.ctx = ctx;
        }

        //View All Table Staff
        //[Authorize]
        public IActionResult Index(int? page)
        {
            int pageSize = 4; // Số lượng mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị

            var paginatedCourses = _userRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_userRepository.GetAll().Count() / pageSize);

            return View("Index", paginatedCourses);
        }
        // Detail
        public IActionResult PopupView(int id)
        {
            var model = _userRepository.GetByID(id);
            return PartialView("_PopupUserViewPartial", model);
        }
        public IActionResult Search(string keyword)
        {
            List<User> users;

            if (string.IsNullOrEmpty(keyword))
            {
                users = ctx.Users.ToList();
            }
            else
            {
                users = ctx.Users
            .Where(p =>
                p.UserName.Contains(keyword) ||
                p.PhoneNumber.Contains(keyword) ||
                p.Email.Contains(keyword)).ToList();
            }

            if (users.Count == 0)
            {
                ViewBag.SearchMessage = "Không tìm thấy Staff nào!";
            }

            // Chuyển hướng về trang Index với trang hiện tại
            return View("Index", users);
        }
        // Delete
        public IActionResult Delete(int id)
        {
            try
            {
                _userRepository.Delete(id);
                TempData["userSuccess"] = "Delete successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["userError"] = $"Error delete User: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExportCsv()
        {
            // Lấy danh sách các bài học từ database
            var users = _userRepository.GetAll();

            // Tạo một StringBuilder để lưu trữ dữ liệu CSV
            StringBuilder csvContent = new StringBuilder();

            // Thêm tiêu đề cột
            csvContent.AppendLine("UserId,UserName,Email,Address,CreateAt,PhoneNumber,Password,UserAsp");

            // Thêm dữ liệu từ danh sách bài học
            foreach (var user in users)
            {
                // Kiểm tra null trước khi truy cập các thuộc tính
                string userId = user.UserId.ToString() ?? "";
                string userName = EscapeCsvField(user.UserName?.ToString() ?? "");
                string email = EscapeCsvField(user.Email?.ToString() ?? "");
                string address = EscapeCsvField(user.Address?.ToString() ?? "");
                string userAsp = EscapeCsvField(user.UserAsp?.ToString() ?? "");
                string createAt = EscapeCsvField(user.CreateAt?.ToString() ?? "");
                string phoneNumber = user.PhoneNumber?.ToString() ?? "";
                string password = user.Password?.ToString() ?? "";

                csvContent.AppendLine($"{userId},{userName},{email},{address},{createAt},{phoneNumber},{password},{userAsp}");
                // Thêm dữ liệu cho các cột khác tùy thuộc vào các trường bạn muốn xuất
            }

            // Chuyển đổi StringBuilder thành mảng byte và trả về file CSV
            byte[] fileContents = Encoding.UTF8.GetBytes(csvContent.ToString());

            return File(fileContents, "text/csv", "User.csv");
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
