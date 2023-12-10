using Azure;
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

            decimal totalDiscountedPrice = 0;
            decimal totalOriginalPrice = 0;
            foreach (var detail in bill.BillDetails)
            {
                totalDiscountedPrice += detail.Price;
                totalOriginalPrice += detail.DiscountPrice ?? detail.Price; 
            }

            var viewModel = new ShoppingCartViewModel
            {
                Bill = bill,
                TotalDiscountedPrice = totalDiscountedPrice,
                TotalOriginalPrice = totalOriginalPrice,
                TotalSaving = totalOriginalPrice - totalDiscountedPrice
            };

            return View(viewModel);
        }

        public IActionResult PurchaseHistory()
        {
            var userId = 4;
            var bill = _billRepository.GetPendingBillByUserId(userId);
            if (bill.Status == "Payment Successful")
            {
                return View("DetailBillSuccess"); // Hiển thị Details Bill khi có trạng thái "Payment Successful"
            }

            var viewModel = new ShoppingCartViewModel
            {
                Bill = bill
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartAsync(int courseId)
        {
            var userId = 4;

            // Kiểm tra xem khóa học có tồn tại không
            var course = _courseRepository.FindCourseByID(courseId);
            if (course == null)
            {
                return Json(new { success = false, message = "Khóa học không tồn tại." });
            }

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

            // Kiểm tra xem khóa học đã có trong hóa đơn "pending" chưa
            var existingBillDetail = await _billRepository.GetBillDetailByCourseAndUser(courseId, userId, bill.BillId);
            if (existingBillDetail != null)
            {
                return Json(new { success = false, message = "Khóa học này đã có trong giỏ hàng của bạn." });
            }


            var billDetail = new BillDetail
            {
                BillDetailId = Guid.NewGuid(),
                BillId = bill.BillId,
                CourseId = course.CourseId,
                Price = course.Price,
                DiscountPrice = course.DiscountPrice,
                Date = DateTime.Now
            };
            _billRepository.AddBillDetail(billDetail);
            // Lấy số lượng sản phẩm mới trong giỏ hàng sau khi thêm
            var cartCount = await _billRepository.GetCartCountAsync(userId);

            return Json(new { success = true, cartCount = cartCount, message = "Thêm vào giỏ hàng thành công!" });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int courseId)
        {
            var userId = 4;

            try
            {
                var bill = _billRepository.GetPendingBillByUserId(userId);
                if (bill == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy giỏ hàng.", redirectUrl = Url.Action("EmptyCart", "ShoppingCart") });
                }

                var billDetail = bill.BillDetails.FirstOrDefault(bd => bd.CourseId == courseId);
                if (billDetail == null)
                {
                    return Json(new { success = false, message = "Khóa học không tồn tại trong giỏ hàng." });
                }

                _billRepository.RemoveBillDetail(billDetail.BillDetailId);

                if (!bill.BillDetails.Any())
                {
                    _billRepository.DeleteBill(bill.BillId);
                    return Json(new { success = true, cartCount = 0 });
                }

                var totalDiscountedPrice = bill.BillDetails.Sum(bd => bd.Price);
                var totalOriginalPrice = bill.BillDetails.Sum(bd => bd.DiscountPrice ?? bd.Price);
                var totalSaving = totalOriginalPrice - totalDiscountedPrice;
                var cartCount = bill.BillDetails.Count;

                return Json(new
                {
                    success = true,
                    message = "Đã xóa khóa học khỏi giỏ hàng.",
                    totalDiscountedPrice = totalDiscountedPrice.ToString("#,##0"),
                    totalOriginalPrice = totalOriginalPrice.ToString("#,##0"),
                    totalSaving = totalSaving.ToString("#,##0"),
                    percentageDiscount = ((totalSaving / totalOriginalPrice) * 100).ToString("F0"),
                    cartCount = cartCount
                });
            }
            catch (Exception ex)
            {
                // Log exception here
                return Json(new { success = false, message = $"Đã xảy ra lỗi: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Checkout(ShoppingCartViewModel model)
        {

            var paymentMethod = model.SelectedPaymentMethod;
            // Xử lý logic thanh toán tại đây dựa vào phương thức thanh toán đã chọn
            // ...

            // Sau khi xử lý xong, bạn có thể chuyển hướng đến trang xác nhận hoặc hiển thị thông báo lỗi tùy theo kết quả
            return RedirectToAction("PaymentConfirmation"); // Hoặc trả về View với thông báo lỗi
        }

        public IActionResult PaymentConfirmation()
        {

            return View(); 
        }

        public IActionResult EmptyCart()
        {
            return View();
        }

    }
}
