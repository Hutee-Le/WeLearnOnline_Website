using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface ICategoryRepository
    {
        // Retrieve all root categories
        public List<Category> GetRootCategories();

        // Retrieve all second-level categories based on the parent id
        public List<Category> GetSecondLevelCategories(int id);

        // Retrieve all third-level categories based on the parent id
        List<Category> GetThirdLevelCategories(int id);

        // Retrieve all categories
        List<Category> GetAllCategories();

        //Retrieve a category by its id
        public Category GetById(int id);

        // Create a new category
        public void Create(Category category);

        // Update an existing category
        public void Update(Category category);

        // Delete a category by its id
        public void Delete(int id);
    }
}
