using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class StaffRepository : IStaffRepository
        {
            private readonly StWelearnContext _context;

            public StaffRepository(StWelearnContext context)
            {
                _context = context;
            }
         
            public bool Add(Staff staff)
            {
                _context.Staff.Add(staff);
                _context.SaveChanges();
                return true;
            }

            public bool Update(Staff staff)
            {
                Staff s = _context.Staff.Where(x => x.StaffId == staff.StaffId).FirstOrDefault();
                if (s != null)
                {
                    _context.Entry(s).CurrentValues.SetValues(staff);
                    _context.SaveChanges();
                    return true;
                }
                return false;                   
            }

            public bool Delete(string id)
            {
                Staff s = _context.Staff.Where(x => x.StaffId == id).FirstOrDefault();
                if (s != null)
                {
                    _context.Staff.Remove(s);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }

            public void DeleteAll()
            {
                throw new NotImplementedException();
            }

            public List<Staff> GetAllStaffs()
            {
               //return _context.Staff.Include(x => x.StaffId).ToList();
               return _context.Staff.ToList();
            }
    }
}
