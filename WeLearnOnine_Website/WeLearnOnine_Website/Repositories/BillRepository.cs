using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly DerekmodeWeLearnSystemContext _context;
        public BillRepository(DerekmodeWeLearnSystemContext context)
        {
            _context = context;
        }

        public Bill GetPendingBillByUserId(int userId)
        {
            return _context.Bills
                .Include(b => b.BillDetails)
                .ThenInclude(bd => bd.Course)
                .ThenInclude(c => c.Level)
                .FirstOrDefault(b => b.UserId == userId && b.Status == "Pending");
        }
    }
}
