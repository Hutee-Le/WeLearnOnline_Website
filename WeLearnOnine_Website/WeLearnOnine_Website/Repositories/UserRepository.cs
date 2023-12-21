using Firebase.Auth;
using Firebase.Storage;
using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Repositories
{
   
        public class UserRepository : IUserRepository
        {
            private readonly DerekmodeWeLearnSystemContext _context;
            private readonly IConfiguration _configuration;

        public UserRepository(DerekmodeWeLearnSystemContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public List<Models.User> GetAll()
            {
                return _context.Users.ToList();
            }



            public Models.User GetById(int id)
            {
                return _context.Users.FirstOrDefault(p => p.UserId == id);
            }
            public void Add(Models.User user)
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                _context.Users.Add(user);
                _context.SaveChanges();
            }

            public void Update(Models.User user)
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                _context.Users.Update(user);
                _context.SaveChanges();
            }

            public void Delete(int id)
            {
                var user = _context.Users.FirstOrDefault(p => p.UserId == id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }

            public Models.User GetByID(int id)
            {
                Models.User c = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
                return c;
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
                var imageChild = imageStorage.Child("images").Child("avatar").Child(imageFileName);

                using (var imageStream = imageFile.OpenReadStream())
                {
                    return await imageChild.PutAsync(imageStream);
                }
            }
    }
}

