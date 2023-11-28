using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBillRepository _billRepository;
        private readonly ICourseRepository _courseRepository;
        public ShoppingCartController(IBillRepository billRepository, ICourseRepository courseRepository)
        {
            _billRepository = billRepository;
            _courseRepository = courseRepository;
        }
        public IActionResult Index()
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Account"); // Redirect đến trang đăng nhập
            //}
            var userId = 4;
            var bill = _billRepository.GetPendingBillByUserId(userId);
            if (bill == null)
            {
                return View("EmptyCart"); // Hiển thị giỏ hàng trống nếu không có bill nào
            }

            var viewModel = new ShoppingCartViewModel
            {
                Bill = bill
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToCart(int courseId)
        {
            var userId = 4;

            // Lấy Bill đang ở trạng thái "pending" của người dùng (hoặc tạo mới nếu chưa có)
            var bill = _billRepository.GetPendingBillByUserId(userId);
            if (bill == null)
            {
                bill = new Bill
                {
                    BillId = Guid.NewGuid(),
                    UserId = userId,
                    Total = 0,
                    HistoricalCost = 0,
                    Promotion = 0,
                    CreateAt = DateTime.Now,
                    PaymentMethod = "Bank Transfer",
                    Status = "Pending"
                };
                _billRepository.CreateBill(bill);
            }

            bool courseAlreadyAdded = bill.BillDetails.Any(bd => bd.CourseId == courseId);
            if (courseAlreadyAdded)
            {
                TempData["Notification"] = "Khóa học đã có trong giỏ hàng.";
                return RedirectToAction("Index", "Course");
            }

            var course = _courseRepository.FindCourseByID(courseId);
            if (course == null)
            {
                // Khóa học không tồn tại
                return NotFound("Không tìm thấy khóa học.");
            }

            var billDetail = new BillDetail
            {
                BillDetailId = Guid.NewGuid(),
                BillId = bill.BillId,
                CourseId = course.CourseId,
                Price = course.Price,
                Date = DateTime.Now
            };
            _billRepository.AddBillDetail(billDetail);
            return RedirectToAction("Index");
            //return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int courseId)
        {
            var userId = 4;

            // Lấy Bill đang ở trạng thái "pending" của người dùng
            var bill = _billRepository.GetPendingBillByUserId(userId);
            if (bill == null)
            {
                return NotFound("Không tìm thấy giỏ hàng.");
            }
            // Tìm BillDetail và xóa nó
            var billDetail = bill.BillDetails.FirstOrDefault(bd => bd.CourseId == courseId);
            if (billDetail != null)
            {
                _billRepository.RemoveBillDetail(billDetail.BillDetailId);

                if (!bill.BillDetails.Any())
                {
                    _billRepository.DeleteBill(bill.BillId);
                }
            }
            else
            {
                return NotFound("Không tìm thấy khóa học trong giỏ hàng.");
            }

            return RedirectToAction("Index");
        }
    }
}
