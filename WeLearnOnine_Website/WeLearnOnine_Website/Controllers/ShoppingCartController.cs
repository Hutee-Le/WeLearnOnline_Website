﻿using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using System.Security.Claims;
using System.Text.Json;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.Services;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBillRepository _billRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly Helper _helper;
        private readonly IEmailService _emailService;
        private readonly HttpClient client = new HttpClient();
        public ShoppingCartController(IBillRepository billRepository,
            ICourseRepository courseRepository
            , IUserRepository userRepository,
            IConfiguration configuration,
            Helper helper, IEmailService emailService)
        {
            _billRepository = billRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _configuration = configuration;
            _helper = helper;
            _emailService = emailService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var claimsPrincipal = HttpContext.User;
            if (User.Identity.IsAuthenticated)
            {
                //var userId = 4;
                int userId = await _helper.GetUserId(claimsPrincipal);
                var bill = _billRepository.GetPendingBillByUserId(userId);
                var user = _userRepository.GetById(userId);
                if (bill == null)
                {
                    return View("EmptyCart"); // Hiển thị giỏ hàng trống nếu không có bill nào
                }

                decimal totalDiscountedPrice = 0;
                decimal totalOriginalPrice = 0;
                foreach (var detail in bill.BillDetails)
                {
                    totalDiscountedPrice += detail.DiscountPrice ?? detail.Price;
                    totalOriginalPrice += detail.Price;
                }

                var viewModel = new ShoppingCartViewModel
                {
                    Bill = bill,
                    TotalDiscountedPrice = totalDiscountedPrice,
                    TotalOriginalPrice = totalOriginalPrice,
                    TotalSaving = totalOriginalPrice - totalDiscountedPrice,
                    UserName = user.UserName,
                    UserEmail = user.Email

                };

                return View(viewModel);
            }
                return RedirectToAction("Login", "User");
            
        }

        public async Task<IActionResult> PurchaseHistoryAsync()
        {
            var claimsPrincipal = HttpContext.User;
            if (User.Identity.IsAuthenticated)
            {
                // var userId = 4;
                int userId = await _helper.GetUserId(claimsPrincipal);
                List<Bill> bill = _billRepository.GetProcessingOrSuccessfulBill(userId);
                var user = _userRepository.GetById(userId);

                var viewModel = new PurchaseHistoryViewModel
                {
                    Bill = bill,
                    UserEmail = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber
                };

            return View(viewModel);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartAsync(int courseId)
        {
            //ar userId = 4;
            var claimsPrincipal = HttpContext.User;
            if (User.Identity.IsAuthenticated)
            {
                int userId = await _helper.GetUserId(claimsPrincipal);
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
                    var todayBillCount = _billRepository.GetBillCountForDate(DateTime.Now);
                    string billCode = Helper.GenerateBillCode(DateTime.Now, todayBillCount);

                    bill = new Bill
                    {
                        BillId = Guid.NewGuid(),
                        UserId = userId,
                        BillCode = billCode,
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
                    CourseId = courseId,
                    Price = course.Price,
                    DiscountPrice = course.DiscountPrice,
                    Date = DateTime.Now
                };
                _billRepository.AddBillDetail(billDetail);
                // Lấy số lượng sản phẩm mới trong giỏ hàng sau khi thêm
                var cartCount = await _billRepository.GetCartCountAsync(userId);

                return Json(new { success = true, cartCount = cartCount, message = "Thêm vào giỏ hàng thành công!" });
            }
            return Json(new { success = false, needAuthentication = true });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCartAsync(int courseId)
        {
            // var userId = 4;
            var claimsPrincipal = HttpContext.User;
            int userId = await _helper.GetUserId(claimsPrincipal);
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

                var totalDiscountedPrice = bill.BillDetails.Sum(bd => bd.DiscountPrice ?? bd.Price);
                var totalOriginalPrice = bill.BillDetails.Sum(bd => bd.Price);
                var totalSaving = totalOriginalPrice - totalDiscountedPrice;
                var cartCount = bill.BillDetails.Count;

                bill.HistoricalCost = totalOriginalPrice;
                bill.Promotion = totalSaving;

                // Lưu các thay đổi vào cơ sở dữ liệu
                _billRepository.UpdateBill(bill);

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
        public async Task<IActionResult> Checkout(ShoppingCartViewModel model)
        {
            //var userId = 4;
            var claimsPrincipal = HttpContext.User;
            int userId = await _helper.GetUserId(claimsPrincipal);
            var bill = _billRepository.GetPendingBillByUserId(userId);

            if (bill == null)
            {
                // Xử lý trường hợp không tìm thấy hóa đơn
                return View("Error"); // Trang lỗi hoặc thông báo phù hợp
            }
            if (model.SelectedPaymentMethod == "MOMO")
            {
                var accessKey = _configuration["MoMoSettings:AccessKey"];
                var secretKey = _configuration["MoMoSettings:SecretKey"];
                var partnerCode = _configuration["MoMoSettings:PartnerCode"];
                var ipnUrl = _configuration["MoMoSettings:ipnUrl"];
                var redirectUrl = _configuration["MoMoSettings:redirectUrl"];
                var orderInfo = _configuration["MoMoSettings:OrderInfo"];
                var requestType = _configuration["MoMoSettings:RequestType"];
                var payGate = _configuration["MoMoSettings:PayGate"];
                var partnerName = _configuration["MoMoSettings:PartnerName"];
                var autoCapture = bool.Parse(_configuration["MoMoSettings:AutoCapture"]);
                var orderExpireTime = int.Parse(_configuration["MoMoSettings:OrderExpireTime"]);
                var lang = _configuration["MoMoSettings:Lang"];


                MoMoRequest request = new MoMoRequest();
                request.orderInfo = orderInfo;
                request.partnerCode = partnerCode;
                request.ipnUrl = ipnUrl + "/ShoppingCart/UpdatePaymentStatus";
                request.redirectUrl = redirectUrl + "/ShoppingCart/PaymentConfirmation?billCode=" + bill.BillCode;
                request.amount = (long)model.TotalDiscountedPrice;
                request.orderId = bill.BillCode;
                request.requestId = bill.BillCode + "id";
                request.requestType = requestType;
                request.extraData = "";
                request.partnerName = partnerName;
                request.autoCapture = autoCapture;
                request.orderExpireTime = orderExpireTime;
                request.lang = lang;
                var rawSignature = "accessKey=" + accessKey + "&amount=" + request.amount + "&extraData=" + request.extraData + "&ipnUrl=" + request.ipnUrl + "&orderId=" + request.orderId + "&orderInfo=" + request.orderInfo + "&partnerCode=" + request.partnerCode + "&redirectUrl=" + request.redirectUrl + "&requestId=" + request.requestId + "&requestType=" + request.requestType;
                request.signature = Helper.getSignature(rawSignature, secretKey);

                StringContent httpContent = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
                var quickPayResponse = await client.PostAsync(payGate, httpContent);

                var responseContent = await quickPayResponse.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<MoMoResponse>(responseContent);

                bill.PayUrl = responseObject.payUrl;
            }

            bill.Status = "Processing";
            bill.PaymentMethod = model.SelectedPaymentMethod;
            bill.HistoricalCost = model.TotalOriginalPrice;
            bill.Promotion = model.TotalOriginalPrice - model.TotalDiscountedPrice;
            bill.Total = model.TotalDiscountedPrice;
            bill.Email = model.UserEmail;

            // Lưu các thay đổi vào cơ sở dữ liệu
            _billRepository.UpdateBill(bill);

            // Gửi email thông báo đơn hàng cần thanh toán
            await _emailService.SendPaymentReminderEmailAsync(
            model.UserEmail,
            model.UserName,
            bill.BillCode
            );

            return RedirectToAction("PaymentConfirmation", new { billCode = bill.BillCode });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePaymentStatusAsync([FromBody] MoMoResponse ipnResponse)
        {
            var bill = _billRepository.FindBillByBillCode(ipnResponse.orderId);
            var user = _userRepository.GetUserById(bill.UserId);
    
            var orderStatus = "";
            if (bill == null)
            {
                return NotFound("Bill not found.");
            }

            if (ipnResponse.resultCode == 0 || ipnResponse.resultCode == 9000)
            {
                bill.Status = "Successful";
                bill.PayType = ipnResponse.payType;
                orderStatus = "thành công";
            }
            else
            {
                bill.Status = "Fail";
               bill.PayType = ipnResponse.payType;
                orderStatus = "không thành công";
            }

            // Cập nhật thông tin thanh toán vào cơ sở dữ liệu
            _billRepository.UpdateBill(bill);

            var courses = bill.BillDetails.Select(bd => bd.Course).ToList();

            await _emailService.SendOrderStatusEmailAsync(bill, user.UserName, orderStatus, courses);

            return Ok();
        }

        public async Task<IActionResult> PaymentConfirmation(string billCode)
        {

            if (!User.Identity.IsAuthenticated) 
            {
                string returnUrl = Url.Action("PaymentConfirmation", "ShoppingCart", new { billCode = billCode });
                return RedirectToAction("Login", "User", new { returnUrl });
            }

            var claimsPrincipal = HttpContext.User;
            int userId = await _helper.GetUserId(claimsPrincipal);
            var bill = _billRepository.FindBillByBillCode(billCode);
            var user = _userRepository.GetById(userId);
            if (bill == null || user == null)
            {
                // Xử lý trường hợp không tìm thấy hóa đơn hoặc thông tin người dùng
                return View("Error");
            }

            if (bill.UserId != userId)
            {
                // Nếu không, chuyển hướng đến trang lỗi hoặc trang không có quyền truy cập
                return RedirectToAction("AccessDenied", "User");
            }

            var viewModel = new ShoppingCartViewModel
            {
                Bill = bill,
                UserEmail = user.Email,
                UserName = user.UserName,
            };

            // Truyền bill đến view
            return View(viewModel);
        }

        public IActionResult EmptyCart()
        {
            return View();
        }

    }
}
