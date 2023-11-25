using Firebase.Auth;
using Firebase.Storage;
using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
	public class LessonRepository : ILessonRepository
	{
		public DerekmodeWeLearnSystemContext _context;
        private readonly IConfiguration _configuration;

        public LessonRepository(DerekmodeWeLearnSystemContext context, IConfiguration configuration)
		{
			_context = context;
            _configuration = configuration;
		}

        public Lesson GetById(int id)
		{
			return _context.Lessons.FirstOrDefault(p => p.LessonId == id);
		}
		public void Add(Lesson lesson)
		{
			if (lesson == null)
				throw new ArgumentNullException(nameof(lesson));

			_context.Lessons.Add(lesson);
			_context.SaveChanges();
		}

		public void Update(Lesson lesson)
		{
			if (lesson == null)
				throw new ArgumentNullException(nameof(lesson));

			_context.Lessons.Update(lesson);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var lesson = _context.Lessons.FirstOrDefault(p => p.LessonId == id);
			if (lesson != null)
			{
				_context.Lessons.Remove(lesson);
				_context.SaveChanges();
			}
		}
		public void DeleteAll()
		{
			throw new NotImplementedException();
		}

        public List<Lesson> GetAllBaiHoc()
        {
            return _context.Lessons.Include(x => x.Type).Include(x => x.Course).ToList();
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
            var imageChild = imageStorage.Child("lesson").Child(imageFileName);

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

        public Lesson getLessonbyCourse(int courseid, int lessonid)
        {
            Lesson? lesson = _context.Lessons.Where(x => x.CourseId == courseid && x.LessonId == lessonid).FirstOrDefault();
            return lesson;
        }
    }
}
