using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class CategoryCourseRepository : ICategoryCourseRepository
    {
        private DerekmodeWeLearnSystemContext _ctx;

        public CategoryCourseRepository(DerekmodeWeLearnSystemContext ctx)
        {
            _ctx = ctx;
        }

        public List<CategoriesCourse> GetAllCategories()
        {
            return _ctx.CategoriesCourses.ToList();
        }

        public List<Category> GetCoursesByCategoryId(int categoryId)
        {
            // Sử dụng LINQ để lấy danh sách Course dựa trên CategoryID
            var categories = _ctx.CategoriesCourses
                .Where(cc => cc.CourseId == categoryId)
                .Select(cc => cc.Categories)
                .ToList();

            return categories;
        }
    }
}
