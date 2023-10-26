using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly DerekmodeWeLearnSystemContext _ctx;

        public StaffRepository(DerekmodeWeLearnSystemContext ctx)
        {
            _ctx = ctx;
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
            Staff c = _ctx.Staff.Find(staff.StaffId);
            if (c != null)
            {
                _ctx.Entry(c).CurrentValues.SetValues(staff);
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
    }
}
