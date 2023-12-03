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

        // Trong BillRepository.cs
        public IEnumerable<Bill> GetAllBillsWithUser()
        {
            var billsWithUsers = from bill in _context.Bills
                                 join user in _context.Users on bill.UserId equals user.UserId
                                 select new Bill
                                 {
                                     BillId = bill.BillId,
                                     Total = bill.Total,
                                     UserId = bill.UserId,
                                     HistoricalCost = bill.HistoricalCost,
                                     Promotion = bill.Promotion,
                                     Email = user.Email,
                                     Status = bill.Status,
                                     CreateAt = bill.CreateAt,
                                     PaymentMethod = bill.PaymentMethod,
                                     CardHolderName = bill.CardHolderName,
                                     ExpirationDate = bill.ExpirationDate
                                 };
            return billsWithUsers.ToList();
        }


        public void UpdateBill(Bill bill)
        {
            // Thực hiện logic để cập nhật thông tin Bill
            _context.Bills.Update(bill);
            _context.SaveChanges();
        }

        // Lấy số lượng các sản phẩm trong giỏ hàng
        public async Task<int> GetCartCountAsync(int userId)
        {
            var itemCount = await _context.BillDetails
                                     .CountAsync(bd => bd.Bill.UserId == userId && bd.Bill.Status == "Pending");

            return itemCount;
        }
    }
}
