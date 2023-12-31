using Firebase.Auth;
using Firebase.Storage;
using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.ViewModels;

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
            .Include(c => c.Lessons)
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

        public bool RatingCourse(Course course)
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

        public List<CourseViewModel> GetAllCourseWithMany()
        {
            var courseQuery = from b in _ctx.Courses
                              join level in _ctx.Levels on b.LevelId equals level.LevelId
                              join staff in _ctx.Staff on b.StaffId equals staff.StaffId
                              select new CourseViewModel
                              {
                                  CourseId = b.CourseId,
                                  StaffName = staff.StaffName,
                                  LevelName = level.Name,
                                  Title = b.Title,
                                  ShortDescription = b.ShortDescription,
                                  Description = b.Description,
                                  Required = b.Required,
                                  ImageCourseUrl = b.ImageCourseUrl,
                                  Price = b.Price,
                                  DiscountPrice = b.DiscountPrice,
                                  PreviewUrl = b.PreviewUrl,
                                  Count = b.Count,
                                  Language = b.Language,
                                  TimeTotal = b.TimeTotal,
                                  Rating = b.Rating,
                                  CategoriesCourses = (from cate_course in _ctx.CategoriesCourses
                                                       join category in _ctx.Categories on cate_course.CategoriesId equals category.CategoriesId
                                                       where cate_course.CourseId == b.CourseId
                                                       select new CategoryCourseViewModel
                                                       {
                                                           CatCourseId = cate_course.CatCourseId,
                                                           CategoriesId = category.CategoriesId,
                                                           CategoryName = category.CategoryName,
                                                       }).ToList(),
                                  Lessons = _ctx.Lessons.Where(lesson => lesson.CourseId == b.CourseId).ToList()
                            };
            var coursesWithDetails = courseQuery.ToList();
            return coursesWithDetails;
        }




        public Course FindCourseByID(int id)
        {
            Course? course = _ctx.Courses
                .Include(c => c.Staff)
                .Include(c => c.Level)
                .Include(c => c.Lessons)
                .Include(c => c.CategoriesCourses)
                .Where(x => x.CourseId == id)
                .FirstOrDefault();
            return course;
        }

        public List<CourseViewModel> GetAvailableAndFavoriteCourses(int userId)
        {
            // Lấy danh sách ID các khóa học đã mua với điều kiện hóa đơn đã được thanh toán
            var purchasedCourseIds = _ctx.BillDetails
                .Where(bd => bd.Bill.UserId == userId && bd.Bill.Status == "Successful") 
                .Select(bd => bd.CourseId)
                .ToHashSet();

            // Danh sách các khóa học yêu thích
            var favoriteCourseIds = _ctx.FavLists
                .Where(f => f.UserId == userId)
                .Select(f => f.CourseId)
                .ToHashSet();

            // Danh sách các khóa học trong giỏ hàng
            var cartCourseIds = _ctx.BillDetails
                .Where(bd => bd.Bill.UserId == userId && bd.Bill.Status == "Pending")
                .Select(bd => bd.CourseId)
                .ToHashSet();

            // Lấy danh sách các khóa học chưa mua
            var availableCourses = _ctx.Courses
                .Where(c => !purchasedCourseIds.Contains(c.CourseId))
                .Include(c => c.Level)
                .Include(c => c.Staff)
                .AsNoTracking()
                .ToList();

            // Tạo và trả về CourseViewModels
            var courseViewModels = availableCourses.Select(c => new CourseViewModel
            {
                Course = c,
                LevelName = c.Level?.Name,
                StaffName = c.Staff?.StaffName,
                IsInFavorites = favoriteCourseIds.Contains(c.CourseId),
                IsInCart = cartCourseIds.Contains(c.CourseId)
            }).ToList();

            return courseViewModels;
        }

        public List<CourseViewModel> GetAllAvailableCourses()
        {
            // Lấy danh sách tất cả các khóa học từ cơ sở dữ liệu
            var courses = _ctx.Courses
                .Include(c => c.Level)
                .Include(c => c.Staff)
                .AsNoTracking()
                .ToList();

            // Tạo danh sách CourseViewModel
            var courseViewModels = courses.Select(c => new CourseViewModel
            {
                Course = c,
                LevelName = c.Level?.Name, 
                StaffName = c.Staff?.StaffName, 
                IsInFavorites = false,
                IsInCart = false
            }).ToList();

            return courseViewModels;
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

        public List<int> GetSelectedCategoriesForCourse(int courseId)
        {
            // Thực hiện truy vấn cơ sở dữ liệu để lấy danh sách các category đã chọn cho khóa học có ID là 'courseId'
            // Giả sử Categories_Course là tên bảng liên kết giữa Courses và Categories
            return _ctx.CategoriesCourses
                .Where(cc => cc.CourseId == courseId)
                .Select(cc => cc.CategoriesId)
                .ToList();
        }

        public CourseViewModel GetCategoryViewModelById(int courseId)
        {
            var CourseViewModel = (from b in _ctx.Courses
                                   join level in _ctx.Levels on b.LevelId equals level.LevelId
                                   join staff in _ctx.Staff on b.StaffId equals staff.StaffId
                                   where b.CourseId == courseId
                                   select new CourseViewModel
                                   {
                                       CourseId = b.CourseId,
                                       StaffName = staff.StaffName,
                                       LevelName = level.Name,
                                       Title = b.Title,
                                       ShortDescription = b.ShortDescription,
                                       Description = b.Description,
                                       Required = b.Required,
                                       ImageCourseUrl = b.ImageCourseUrl,
                                       Price = b.Price,
                                       DiscountPrice = b.DiscountPrice,
                                       PreviewUrl = b.PreviewUrl,
                                       Count = b.Count,
                                       Language = b.Language,
                                       TimeTotal = b.TimeTotal,
                                       Rating = b.Rating,
                                       CategoriesCourses = (from cate_course in _ctx.CategoriesCourses
                                                            join category in _ctx.Categories on cate_course.CategoriesId equals category.CategoriesId
                                                            where cate_course.CourseId == b.CourseId
                                                            select new CategoryCourseViewModel
                                                            {
                                                                CatCourseId = cate_course.CatCourseId,
                                                                CategoriesId = category.CategoriesId,
                                                                CategoryName = category.CategoryName,
                                                            }).ToList(),
                                       Lessons = _ctx.Lessons.Where(lesson => lesson.CourseId == courseId).ToList(),
                                   }).FirstOrDefault();

            return CourseViewModel;
        }

        public bool IsCourseInCart(int courseId, int userId)
        {
            return _ctx.BillDetails
                       .Any(bd => bd.CourseId == courseId &&
                                  bd.Bill.UserId == userId &&
                                  bd.Bill.Status == "Pending");
        }

        public bool IsCourseInFavorites(int courseId, int userId)
        {
            return _ctx.FavLists.Any(f => f.CourseId == courseId && f.UserId == userId);
        }
    }
}
