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

        //View All Table Bill
        public IActionResult Index()
        {
            var billsWithUsers = _billRepository.GetAllBillsWithUser();
            return View("Index", billsWithUsers);
        }

        // Detail
        public IActionResult BillDetailsView(Guid id)
        {
            var model = _billRepository.GetBillViewModelById(id);
            return PartialView("_BillDetailsViewPartial", model);
        }


        // Edit
        [HttpPost]
        public IActionResult UpdateBill(Bill bill)
        {
            try
            {
                _billRepository.UpdateBillStatus(bill);

                TempData["billSuccess"] = "Update Payment Method Bill successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["billError"] = $"Error updating Bill: {ex.Message}";
            }



            return RedirectToAction("Index");
        }

        public IActionResult EditBill(Guid id)
        {
            ViewBag.Status = new SelectList(new List<string>() { "Successful", "Fail"});
            return View("EditBill", _billRepository.GetBillById(id));
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
