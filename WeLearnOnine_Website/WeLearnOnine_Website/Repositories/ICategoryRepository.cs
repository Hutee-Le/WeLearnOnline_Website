using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface ICategoryRepository
    {
        // Retrieve all root categories
        public List<Category> GetRootCategories();

        // Retrieve subcategories for a given category.
        List<Category>  GetSubCategories(int id);

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
