using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Models
{
    public interface ICourseRepository
    {
        bool Add(Course course);
        bool Update(Course course);
        bool IsCourseInCart(int courseId, int userId);
        bool IsCourseInFavorites(int courseId, int userId);
        bool Delete(int id);
        void DeleteAll();
        Course FindCourseByID(int id);
        public IEnumerable<Course> GetTopCourses(int count);
        List<Bill> MyCourses(int userId);

        Task<string> UploadVideoAsync(IFormFile videoFile);
        Task<string> UploadImageAsync(IFormFile imageFile);

        Task<bool> AddToFavorites(int userId, int courseId);

        Task<bool> RemoveFromFavorites(int userId, int courseId);
        List<Course> GetAllCourses();
        List<CourseViewModel> GetAllCourseWithMany();

        // Retrieve all courses by category id
        //List<Course> GetCoursesByCategoryId(int categoryId);
        List<CourseViewModel> GetCoursesByCategoryId(int categoryId);

        List<CourseViewModel> GetAllAvailableCourses();

        // Retrieve all courses with favorite status
        List<CourseViewModel> GetAvailableAndFavoriteCourses(int userId);
        List<Course> Search(string keyword);
        List<int> GetSelectedCategoriesForCourse(int courseId);

        //public List<string> GetCategoriesForCourse(int courseId);
        CourseViewModel GetCategoryViewModelById(int courseId);
    }
}
