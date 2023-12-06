using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class DashboardViewModel
    {
        public List<Course> courses { get; set; }
        public List<Lesson> lessons { get; set; }
        public List<Bill> bills { get; set; }
        public List<Staff> staffs { get; set; }
    }
}
