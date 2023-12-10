using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class CourseViewModel
    {
        public Course? Course { get; set; }
        public string? LevelName { get; set; }
        public string? StaffName { get; set; }
        public bool IsInFavorites { get; set; }
        public bool IsInCart { get; set; }
    }
}
