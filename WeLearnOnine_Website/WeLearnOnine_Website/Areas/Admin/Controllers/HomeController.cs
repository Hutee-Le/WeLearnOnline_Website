using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class HomeController : Controller
    {
        private DerekmodeWeLearnSystemContext ctx;
        private ICourseRepository _courseRepository;
        private ILessonRepository _lessonRepository;
        private ISkillRepository _staffRepository;
        private IBillRepository _billRepository;

        public HomeController(ICourseRepository courseRepository,
            ILessonRepository lessonRepository,
            ISkillRepository staffRepository,
            IBillRepository billRepository, DerekmodeWeLearnSystemContext context)
        {
            _courseRepository = courseRepository;
            _lessonRepository = lessonRepository;
            _staffRepository = staffRepository;
            _billRepository = billRepository;
            ctx = context;
        }
        [Authorize(Roles = "Admin, Instructor, Training")]
        public IActionResult Index()
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Account"); // Redirect đến trang đăng nhập
            //}
            var bill = _billRepository.GetAllBills();

            // Tính tổng giá trị các mục trong Bill
            decimal totalPrice = ctx.Bills.Sum(item => item.Total);

            // Lưu tổng giá trị vào ViewBag để truyền sang View
            ViewBag.TotalPrice = totalPrice;

            var course = _courseRepository.GetAllCourses();
            var lesson = _lessonRepository.GetAllBaiHoc();
            var staff = _staffRepository.GetAllStaffs();
            var viewModel = new DashboardViewModel
            {
                bills = bill,
                courses = course,
                staffs = staff,
                lessons = lesson

            };

            return View(viewModel);
        }
    }
}