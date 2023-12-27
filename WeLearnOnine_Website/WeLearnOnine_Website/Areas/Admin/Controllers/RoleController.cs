using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private IRoleRepository _roleRepository;
        DerekmodeWeLearnSystemContext ctx;

        public RoleController(IRoleRepository roleRepository, DerekmodeWeLearnSystemContext ctx)
        {
            _roleRepository = roleRepository;
            this.ctx = ctx;
        }

        //View All Table Staff
        [Authorize]
        public IActionResult Index()
        {
            var roles = _roleRepository.GetAll().ToList();

            return View("Index", roles);
        }

        [HttpPost]
        public IActionResult SaveRole(AspNetRole aspNetRole)
        {
            // Kiểm tra xem RoleName có giá trị hay không
            if (!string.IsNullOrEmpty(aspNetRole.Name))
            {
                // Set a new Id if it's null
                if (string.IsNullOrEmpty(aspNetRole.Id))
                {
                    aspNetRole.Id = Guid.NewGuid().ToString(); // Set a new GUID as the Id
                }

                _roleRepository.Add(aspNetRole);

                TempData["roleSuccess"] = "Role successfully saved!";
            }
            else
            {
                TempData["roleError"] = "Name Role cannot be null or empty!";
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult CreateRole()
        {
            return View("CreateRole", new AspNetRole());
        }



        [HttpPost]
        // Edit
        public IActionResult UpdateRole(AspNetRole aspNetRole)
        {
            // Kiểm tra xem RoleName có giá trị hay không
            if (!string.IsNullOrEmpty(aspNetRole.Name))
            {
                // Ensure Id is not null
                if (string.IsNullOrEmpty(aspNetRole.Id))
                {
                    TempData["roleError"] = "Role Id cannot be null or empty!";
                    return RedirectToAction("Index");
                }

                _roleRepository.Update(aspNetRole);

                TempData["roleSuccess"] = "Role successfully saved!";
            }
            else
            {
                TempData["roleError"] = "Role Name cannot be null or empty!";
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditRole(Guid id)
        {
            return View("EditRole", _roleRepository.GetByID(id));
        }

        // Delete
        public IActionResult Delete(Guid id)
        {
            try
            {
                _roleRepository.Delete(id);
                TempData["roleSuccess"] = "Delete successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["roleError"] = $"Error delete Role: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

      
    }
}
