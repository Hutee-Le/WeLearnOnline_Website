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

        // Retrieve all courses with favorite status
        List<CourseViewModel> GetCoursesWithFavoriteStatus(int userId);

        //List<Course> GetAllCoursesWithStaff();
        List<Course> Search(string keyword);
        //List<Course> GetCoursesFavoriteStatus(int userId);
    }
}
