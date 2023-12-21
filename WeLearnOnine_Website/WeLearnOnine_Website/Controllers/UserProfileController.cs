using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Controllers
{
    public class UserProfileController : Controller
    {
      
        private IUserRepository _userRepository;
        public UserProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var id = 4;
            var infoUser = _userRepository.GetById(id);
            return View("Index", infoUser);
        }

        // Edit
        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user, IFormFile ImageAvartarFile, string ExistingImageAvartarFile)
        {
            try
            {
                if (ImageAvartarFile != null)
                {
                    user.Avatar = await _userRepository.UploadImageAsync(ImageAvartarFile);
                }
                else
                {
                    // Sử dụng giá trị hiện tại nếu không có file mới
                    user.Avatar = ExistingImageAvartarFile;
                }

                _userRepository.Update(user);

                TempData["userSuccess"] = "Edit Profile successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["userError"] = $"Error Updating Profile: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
        public IActionResult EditUser()
        {
            var id = 4;
            return View("EditUser", _userRepository.GetById(id));
        }
    }
}
