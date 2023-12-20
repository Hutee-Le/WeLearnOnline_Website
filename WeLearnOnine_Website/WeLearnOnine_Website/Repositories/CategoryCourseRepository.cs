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

        public List<Category> GetCategoriesForCourse(int courseId)
        {
            var categories = _ctx.CategoriesCourses
                .Where(cc => cc.CourseId == courseId)
                .Join(
                    _ctx.Categories,
                    cc => cc.CategoriesId,
                    c => c.CategoriesId,
                    (cc, c) => new Category
                    {
                        CategoriesId = c.CategoriesId,
                        CategoryName = c.CategoryName,
                        // Các thuộc tính khác của Category bạn muốn lấy
                    }
                )
                .ToList();

            return categories;
        }

    }
}
