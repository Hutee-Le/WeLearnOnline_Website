using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class CategoryCourseViewModel
	{
        public List<Course> Courses { get; set; }
        public List<Category> Categories { get; set; }

        public int? SelectedCategoryId { get; set; } 
    }
}
