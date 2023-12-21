using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        User GetByID(int id);
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}
