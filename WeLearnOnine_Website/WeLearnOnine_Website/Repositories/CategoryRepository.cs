using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DerekmodeWeLearnSystemContext _ctx;

        public CategoryRepository (DerekmodeWeLearnSystemContext ctx)
        {
            _ctx = ctx;
        }

        public List<Category> GetRootCategories()
        {
            return _ctx.Categories
                .Where(c => c.ParentCategories == null)
                .OrderBy(c => c.CategoryName)
                .ToList();
        }

        public List<Category> GetSecondLevelCategories(int id)
        {
            return _ctx.Categories
                .Where(c=>c.ParentCategories==id)
                .OrderBy(c => c.CategoryName)
                .ToList();
        }

        public List<Category> GetThirdLevelCategories(int id)
        {
            return _ctx.Categories
            .Where(c => c.ParentCategories == id)
            .OrderBy(c => c.CategoryName)
            .ToList();
        }

        public List<Category> GetAllCategories()
        {
            return _ctx.Categories.ToList();
        }

        public Category GetById(int id)
        {
            var category = _ctx.Categories.FirstOrDefault(c => c.CategoriesId == id);
            if (category == null)
            {
                throw new InvalidOperationException("Category not found.");
            }
            return category;
        }

        public void Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var existingCategory = _ctx.Categories.FirstOrDefault(c => c.CategoryName == category.CategoryName);
            if (existingCategory != null)
            {
                // Xử lý trường hợp tên danh mục đã tồn tại
                throw new InvalidOperationException("Category with the same name already exists.");
            }

            _ctx.Categories.Add(category);
            _ctx.SaveChanges();
        }

        public void Update(Category category)
        {
            if (category != null)
            {
                var existingCategory = _ctx.Categories.Find(category.CategoriesId);
                if (existingCategory != null)
                {
                    // Sử dụng Entry để theo dõi thay đổi và chỉ cập nhật những trường cần thiết.
                    _ctx.Entry(existingCategory).CurrentValues.SetValues(category);
                    _ctx.SaveChanges();
                }
                else
                {
                    // Xử lý trường hợp danh mục không tồn tại
                    throw new InvalidOperationException("Category not found.");
                }
            }
            else
            {
                // Xử lý trường hợp category là null
                throw new ArgumentNullException(nameof(category));
            }
        }

        public void Delete(int id)
        {
            var category = _ctx.Categories.FirstOrDefault(c => c.CategoriesId == id);
            if (category != null)
            {
                _ctx.Categories.Remove(category);
                _ctx.SaveChanges();
            }
            else
            {
                // Xử lý trường hợp danh mục không tồn tại
                throw new InvalidOperationException("Category not found.");
            }
        }

        
    }
}
