using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
   
        public class UserRepository : IUserRepository
        {
            private readonly DerekmodeWeLearnSystemContext _context;

            public UserRepository(DerekmodeWeLearnSystemContext context)
            {
                _context = context;
            }

            public List<User> GetAll()
            {
                return _context.Users.ToList();
            }



            public User GetById(int id)
            {
                return _context.Users.FirstOrDefault(p => p.UserId == id);
            }
            public void Add(User user)
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                _context.Users.Add(user);
                _context.SaveChanges();
            }

            public void Update(User user)
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
        }
    }

