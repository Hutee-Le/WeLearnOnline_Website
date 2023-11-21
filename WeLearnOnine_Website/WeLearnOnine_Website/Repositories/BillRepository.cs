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

        public void AddBillDetail(BillDetail billDetail)
        {
            _context.BillDetails.Add(billDetail);
            _context.SaveChanges();
        }

        public void CreateBill(Bill bill)
        {
            // Thực hiện logic để thêm Bill mới
            _context.Bills.Add(bill);
            _context.SaveChanges();
        }

        public void DeleteBill(Guid billId)
        {
            // Thực hiện logic để xóa Bill theo ID
            var billToDelete = _context.Bills.FirstOrDefault(b => b.BillId == billId);
            if (billToDelete != null)
            {
                _context.Bills.Remove(billToDelete);
                _context.SaveChanges();
            }
        }

        public Bill GetBillById(Guid billId)
        {
            // Thực hiện logic để lấy Bill theo ID
            return _context.Bills.FirstOrDefault(b => b.BillId == billId);
        }

        public Bill GetPendingBillByUserId(int userId)
        {
            // Thực hiện logic để lấy Bill đang ở trạng thái "pending" của người dùng
            return _context.Bills
                .Include(b => b.BillDetails)
                .ThenInclude(bd => bd.Course)
                .ThenInclude(c => c.Level)
                .FirstOrDefault(b => b.UserId == userId && b.Status == "Pending");
        }

        public void RemoveBillDetail(Guid billDetailId)
        {
            var billDetail = _context.BillDetails.FirstOrDefault(bd => bd.BillDetailId == billDetailId);
            if (billDetail != null)
            {
                _context.BillDetails.Remove(billDetail);
                _context.SaveChanges();
            }
        }

        public void UpdateBill(Bill bill)
        {
            // Thực hiện logic để cập nhật thông tin Bill
            _context.Bills.Update(bill);
            _context.SaveChanges();
        }
    }
}
