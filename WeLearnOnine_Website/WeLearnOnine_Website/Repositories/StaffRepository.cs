using Firebase.Auth;
using Firebase.Storage;
using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class StaffRepository : ISkillRepository
    {
        private readonly DerekmodeWeLearnSystemContext _ctx;
        private readonly IConfiguration _configuration;

        public StaffRepository(DerekmodeWeLearnSystemContext ctx, IConfiguration configuration)
        {
            _ctx = ctx;
            _configuration = configuration;
        }

        public List<Staff> Get(int categoryId)
        {
            var staffs = _ctx.Staff.ToList();
            return staffs;
        }
        public List<Staff> Search(string keyword)
        {
            return _ctx.Staff.Where(c => c.StaffName.Contains(keyword)).ToList();
        }
        public bool Add(Staff staff)
        {
            _ctx.Staff.Add(staff);
            _ctx.SaveChanges();
            return true;
        }

        public bool Update(Staff staff)
        {
            Staff? s = _ctx.Staff.Find(staff.StaffId);
            if (s != null)
            {
                _ctx.Entry(s).CurrentValues.SetValues(staff);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(string id)
        {
            Staff s = _ctx.Staff.Where(x => x.StaffId == id).FirstOrDefault();
            if (s != null)
            {
                _ctx.Staff.Remove(s);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public List<Staff> GetAllStaffs() => _ctx.Staff.ToList();

        public Staff FindStaffByID(string id)
        {
            Staff c = _ctx.Staff.Where(x => x.StaffId == id).FirstOrDefault();
            return c;
        }

        public async Task<string> UploadImageAsync(IFormFile AvatarUrl)
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

            var imageFileName = Path.GetFileName(AvatarUrl.FileName);
            var imageChild = imageStorage.Child("images").Child("Avatar").Child(imageFileName);

            using (var imageStream = AvatarUrl.OpenReadStream())
            {
                return await imageChild.PutAsync(imageStream);
            }
        }
    }
}
