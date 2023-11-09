using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class DetailCourseViewModel
    {
        public Course? Course { get; set; }
        public List<Comment>? Comments { get; set; }
        public string ContentNote { get; set; }
        public int CourseId { get; set; }
        public string? Title { get; set; }
        
        //public User? User { get; set; }
        public string? StaffId { get; set; }
        public string? StaffName { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
    }
}
