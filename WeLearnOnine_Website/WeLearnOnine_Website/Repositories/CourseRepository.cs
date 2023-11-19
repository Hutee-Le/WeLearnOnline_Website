using Firebase.Auth;
using Firebase.Storage;
using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DerekmodeWeLearnSystemContext _ctx;
        private readonly IConfiguration _configuration;

        public CourseRepository(DerekmodeWeLearnSystemContext ctx, IConfiguration configuration)
        {
            _ctx = ctx;
            _configuration = configuration;
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
        public IEnumerable<Course> GetTopCourses(int count)
        {
            return _ctx.Courses.Include(x => x.Level).Include(x => x.Staff).Take(count).ToList();
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
            Course? c = _ctx.Courses.Find(course.CourseId);
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
            Course? f = _ctx.Courses.Where(x => x.CourseId == id).FirstOrDefault();
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
            Course? course = _ctx.Courses
                .Include(c => c.Staff)
                .Include(c => c.Level)
                .Include(c => c.Lessons)
                .Where(x => x.CourseId == id)
                .FirstOrDefault();
            return course;
        }

        public List<CourseViewModel> GetAllCoursesWithDetails(int userId)
        {
            var purchasedCourseIds = _ctx.Bills
                .Where(b => b.UserId == userId)
                .SelectMany(b => b.BillDetails)
                .Select(bd => bd.CourseId)
                .ToList();

            var courses = _ctx.Courses
                .Where(c => !purchasedCourseIds.Contains(c.CourseId))
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

        public async Task<bool> AddToFavorites(int userId, int courseId)
        {
            var existingFavorite = await _ctx.FavLists
                                                 .FirstOrDefaultAsync(f => f.UserId == userId && f.CourseId == courseId);
            if (existingFavorite != null)
            {
                // Khóa học đã có trong danh sách yêu thích
                return false;
            }

            var favorite = new FavList
            {
                UserId = userId,
                CourseId = courseId
            };

            _ctx.FavLists.Add(favorite);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromFavorites(int userId, int courseId)
        {
            var favorite = await _ctx.FavLists
                                         .FirstOrDefaultAsync(f => f.UserId == userId && f.CourseId == courseId);
            if (favorite == null)
            {
                // Khóa học không có trong danh sách yêu thích
                return false;
            }

            _ctx.FavLists.Remove(favorite);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public List<Bill> MyCourses(int userId)
        {
            List<Bill> lst = _ctx.Bills.Where(b => b.UserId == userId)
                 .Include(c => c.BillDetails)
                .ThenInclude(c => c.Course).ToList();
            return lst;
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            var apiKey = _configuration["FirebaseConfig:ApiKey"];
            var bucket = _configuration["FirebaseConfig:Bucket"];
            var authEmail = _configuration["FirebaseConfig:AuthEmail"];
            var authPassword = _configuration["FirebaseConfig:AuthPassword"];

            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(authEmail, authPassword);

            var imageStorage = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                });

            var imageFileName = Path.GetFileName(imageFile.FileName);
            var imageChild = imageStorage.Child("images").Child(imageFileName);

            using (var imageStream = imageFile.OpenReadStream())
            {
                return await imageChild.PutAsync(imageStream);
            }
        }

        public async Task<string> UploadVideoAsync(IFormFile videoFile)
        {
            var apiKey = _configuration["FirebaseConfig:ApiKey"];
            var bucket = _configuration["FirebaseConfig:Bucket"];
            var authEmail = _configuration["FirebaseConfig:AuthEmail"];
            var authPassword = _configuration["FirebaseConfig:AuthPassword"];

            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(authEmail, authPassword);

            var videoStorage = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                });

            var videoFileName = Path.GetFileName(videoFile.FileName);
            var videoChild = videoStorage.Child("videos").Child(videoFileName);

            using (var videoStream = videoFile.OpenReadStream())
            {
                return await videoChild.PutAsync(videoStream);
            }
        }
    }
}
