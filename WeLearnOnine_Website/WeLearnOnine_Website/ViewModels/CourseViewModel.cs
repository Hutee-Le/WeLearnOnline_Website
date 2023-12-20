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

        public int CourseId { get; set; }

        public int? LevelId { get; set; }

        public string StaffId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? Required { get; set; }

        public string? ImageCourseUrl { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public string? PreviewUrl { get; set; }

        public bool Status { get; set; }

        public int? Count { get; set; }

        public double? Rating { get; set; }

        public string? Language { get; set; }

        public int? TimeTotal { get; set; }

        public List<CategoryCourseViewModel> CategoriesCourses { get; set; }
    }
}
