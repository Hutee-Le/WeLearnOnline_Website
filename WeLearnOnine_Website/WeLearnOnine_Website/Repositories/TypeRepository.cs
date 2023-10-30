using WeLearnOnine_Website.Models;
using Type = WeLearnOnine_Website.Models.Type;

namespace WeLearnOnine_Website.Repositories
{
  
        public class TypeRepository : ITypeRepository
    {
            private readonly DerekmodeWeLearnSystemContext _context;

            public TypeRepository(DerekmodeWeLearnSystemContext context)
            {
                _context = context;
            }

            public List<Type> GetAll()
            {
                return _context.Types.ToList();
            }



            public Type GetById(int id)
            {
                return _context.Types.FirstOrDefault(p => p.TypeId == id);
            }
            public void Add(Type type)
            {
                if (type == null)
                    throw new ArgumentNullException(nameof(type));

                _context.Types.Add(type);
                _context.SaveChanges();
            }

            public void Update(Type type)
            {
                if (type == null)
                    throw new ArgumentNullException(nameof(type));

                _context.Types.Update(type);
                _context.SaveChanges();
            }

            public void Delete(int id)
            {
                var type = _context.Types.FirstOrDefault(p => p.TypeId == id);
                if (type != null)
                {
                    _context.Types.Remove(type);
                    _context.SaveChanges();
                }
            }


        }
    }

