    using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DerekmodeWeLearnSystemContext _ctx;

        public CourseRepository(DerekmodeWeLearnSystemContext ctx)
        {
            _ctx = ctx;
        }

        public List<Course> GetCoursesByCategoryId(int categoryId)
        {
            var courses = _ctx.Courses
            .Where(c => c.CategoriesCourses.Any(cc => cc.CategoriesId == categoryId))
            .Include(c => c.Staff)  
            .Include(c => c.Level)
            .ToList();

            return courses;
        }
        public List<Course> Search(string keyword)
        {
            return _ctx.Courses.Where(c => c.Title.Contains(keyword)).ToList();
        }
        public bool Add(Course course)
        {
            _ctx.Courses.Add(course);
            _ctx.SaveChanges();
            return true;
        }

        public bool Update(Course course)
        {
            Course c = _ctx.Courses.Find(course.CourseId);
            if (c != null)
            {
                _ctx.Entry(c).CurrentValues.SetValues(course);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            Course f = _ctx.Courses.Where(x => x.CourseId == id).FirstOrDefault();
            if (f != null)
            {
                _ctx.Courses.Remove(f);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAllCourses() => _ctx.Courses.Include(x => x.Level).Include(x => x.Staff).ToList();

        public Course FindCourseByID(int id)
        {
            Course c = _ctx.Courses.Where(x => x.CourseId == id).FirstOrDefault();
            return c;
        }

        public List<CourseViewModel> GetAllCoursesWithDetails(int userId)
        {
            var courses = _ctx.Courses
                .Include(c => c.Level)
                .Include(c => c.Staff)
                .Select(course => new CourseViewModel
                {
                    Course = course,
                    LevelName = course.Level.Name,
                    StaffName = course.Staff.StaffName,
                    IsInFavorites = _ctx.FavLists.Any(f => f.UserId == userId && f.CourseId == course.CourseId)
                })
                .ToList();

            return courses;
        }

        public List<CourseViewModel> GetAllCoursesWithDetails()
        {
            var courses = _ctx.Courses
        .Include(c => c.Level)
        .Include(c => c.Staff)
        .Select(course => new CourseViewModel
        {
            Course = course,
            LevelName = course.Level.Name,
            StaffName = course.Staff.StaffName,
            IsInFavorites = false 
        })
        .ToList();

            return courses;
        }
    }
}
