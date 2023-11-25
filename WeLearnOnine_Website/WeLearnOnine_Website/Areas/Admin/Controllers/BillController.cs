using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    }
}
