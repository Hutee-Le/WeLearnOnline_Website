using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private IBillRepository _billRepository;
        private IUserRepository _userRepository;

        public BillController(IBillRepository billRepository, IUserRepository userRepository)
        {
            _billRepository = billRepository;
            _userRepository = userRepository;
        }

        //View All Table Staff
        //public IActionResult Index(int? page)
        //{
        //    int pageSize = 4; // Số lượng mục trên mỗi trang
        //    int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị

        //    var paginatedBills = _billRepository.GetAllBill().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        //    ViewBag.CurrentPage = pageNumber;
        //    ViewBag.TotalPages = (int)Math.Ceiling((double)_billRepository.GetAllBill().Count() / pageSize);

        //    return View("Index", paginatedBills);
        //}

        public IActionResult Index()
        {
            var billsWithUsers = _billRepository.GetAllBillsWithUser();

            var userNames = _userRepository.GetAll(); // Đổi tên biến để phản ánh là danh sách tên người dùng
            ViewBag.UserNames = new SelectList(userNames, "UserId", "UserName");

            return View("Index", billsWithUsers);
        }

        // Detail
        public IActionResult BillDetailsView(Guid id)
        {
            var model = _billRepository.GetBillById(id);
            return PartialView("_BillDetailsViewPartial", model);
        }

        // Delete
        public IActionResult Delete(Guid id)
        {
            try
            {
                _billRepository.DeleteBill(id);
                TempData["billSuccess"] = "Delete successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["billError"] = $"Error delete bill: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExportCsv()
        {
            // Lấy danh sách các bài học từ database
            var bills = _billRepository.GetAllBills();

            // Tạo một StringBuilder để lưu trữ dữ liệu CSV
            StringBuilder csvContent = new StringBuilder();

            // Thêm tiêu đề cột
            csvContent.AppendLine("BillId,BillCode,UserId,PaymentMethod,HistoricalCost,Promotion,Total,CreateAt,ExpirationDate,Status,CardHolderName");

            // Thêm dữ liệu từ danh sách bài học
            foreach (var bill in bills)
            {
                // Kiểm tra null trước khi truy cập các thuộc tính
                string billId = bill.BillId.ToString() ?? "";
                string billCode = bill.BillCode.ToString() ?? "";
                string userId = EscapeCsvField(bill.UserId?.ToString() ?? "");
                string total = bill.Total.ToString() ?? "";
                string historicalCost = bill.HistoricalCost.ToString() ?? "";
                string promotion = bill.Promotion.ToString() ?? "";
                string createAt = bill.CreateAt?.ToString() ?? "";
                string expirationDate = bill.ExpirationDate?.ToString() ?? "";
                string status = bill.Status?.ToString() ?? "";
                string paymentMethod = EscapeCsvField(bill.PaymentMethod?.ToString() ?? "");
                string cardHolderName = EscapeCsvField(bill.CardHolderName?.ToString() ?? "");

                csvContent.AppendLine($"{billId},{billCode},{userId},{paymentMethod},{historicalCost},{promotion},{total},{createAt},{expirationDate},{status},{cardHolderName}");
                // Thêm dữ liệu cho các cột khác tùy thuộc vào các trường bạn muốn xuất
            }

            // Chuyển đổi StringBuilder thành mảng byte và trả về file CSV
            byte[] fileContents = Encoding.UTF8.GetBytes(csvContent.ToString());

            return File(fileContents, "text/csv", "Bill.csv");
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
