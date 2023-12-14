using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface ICategoryCourseRepository
    {
      

        // Retrieve all categories
        List<CategoriesCourse> GetAllCategories();

        //Retrieve a category by its id
        List<Category> GetCoursesByCategoryId(int categoryId);
    }
}
