namespace WeLearnOnine_Website.Models
{
    public interface ICourseRepository
    {
        bool Add(Course course);
        bool Update(Course course);
        bool Delete(int id);
        void DeleteAll();
        Course FindCourseByID(int id);
        List<Course> GetAllCourses();

        // Retrieve all courses by category id
        List<Course> GetCoursesByCategoryId(int categoryId);

        List<CourseViewModel> GetAllCoursesWithDetails();

        // Retrieve all courses with favorite status
        List<CourseViewModel> GetAllCoursesWithDetails(int userId);

        //List<Course> GetAllCoursesWithStaff();
        List<Course> Search(string keyword);
       
    }
}
