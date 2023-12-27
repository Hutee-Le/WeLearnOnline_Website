using Microsoft.AspNetCore.Mvc.Rendering;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class UserRoleViewModel
    {
        public string RoleName { get; set; }
        public string UserEmail { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> UserRoles { get; set; }
        public List<SelectListItem> AllRoles { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}
