using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AspUserController : Controller
    {
        private IRoleRepository _roleRepository;
        DerekmodeWeLearnSystemContext ctx;

        public AspUserController(IRoleRepository roleRepository, DerekmodeWeLearnSystemContext ctx)
        {
            _roleRepository = roleRepository;
            this.ctx = ctx;
        }

        //View All Table Staff
        [Authorize]
        public IActionResult Index()
        {
            // Lấy thông tin từ DbContext (DbSet<AspNetUser> và DbSet<AspNetRole>)
            var userRoles = ctx.AspNetUsers
                .SelectMany(user => user.Roles, (user, role) => new UserRoleViewModel
                {
                    RoleName = role.Name,
                    UserEmail = user.Email
                })
                .ToList();

            return View(userRoles);
        }

        public IActionResult ManageUserRoles(Guid userId)
        {
            // Lấy thông tin người dùng theo Id
            var user = ctx.AspNetUsers
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Id == userId.ToString());

            if (user == null)
            {
                return NotFound();
            }

            // Lấy danh sách vai trò từ DbContext
            var roles = ctx.AspNetRoles.ToList();

            // Chuyển đổi danh sách vai trò sang SelectListItem để sử dụng trong dropdown list
            var roleSelectList = roles.Select(r => new SelectListItem
            {
                Value = r.Id,
                Text = r.Name
            }).ToList();

            // Tạo ViewModel để truyền dữ liệu đến view
            var viewModel = new UserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = user.Roles.Select(r => r.Name).ToList(),
                AllRoles = roleSelectList
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ManageUserRoles(UserRoleViewModel viewModel)
        {
            // Lấy thông tin người dùng theo Id
            var user = ctx.AspNetUsers
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Id == viewModel.UserId);

            if (user == null)
            {
                return NotFound();
            }

            // Xóa tất cả vai trò hiện tại của người dùng
            user.Roles.Clear();

            // Thêm lại vai trò mới từ danh sách được chọn
            foreach (var selectedRole in viewModel.SelectedRoles)
            {
                var role = ctx.AspNetRoles.Find(selectedRole);
                if (role != null)
                {
                    user.Roles.Add(role);
                }
            }

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

