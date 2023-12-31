using MailKit;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class DetailCourseViewModel
    {
        public Course? Course { get; set; }
        public List<Comment>? Comments { get; set; }
        public string? ContentNote { get; set; }
        public int CourseId { get; set; }
        public string? Title { get; set; }
        
        public string? StaffId { get; set; }
        public string? StaffName { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public Lesson? Currentlesson { get; set; }
        public string? UserName { get; set; }
        public int LessonId { get; set; }
        public float Star {  get; set; }
        public bool IsInCart { get; set; }
        //public List<CommentViewModel>? Comment { get; set; }
        public class RatingViewModel
        {
        }
        
     
    }
}
