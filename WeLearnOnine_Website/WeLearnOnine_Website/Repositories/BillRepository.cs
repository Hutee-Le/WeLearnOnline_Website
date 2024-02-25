using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.ViewModels;

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
            var billToDelete = _context.Bills.Include(b => b.BillDetails).FirstOrDefault(b => b.BillId == billId);

            if (billToDelete != null)
            {
                // Remove related BillDetails
                _context.BillDetails.RemoveRange(billToDelete.BillDetails);

                // Remove the Bill
                _context.Bills.Remove(billToDelete);

                _context.SaveChanges();
            }
        }

        public Bill GetBillById(Guid billId)
        {
            // Thực hiện logic để lấy Bill theo ID
            return _context.Bills.FirstOrDefault(b => b.BillId == billId);
        }

        public BillViewModel GetBillViewModelById(Guid billId)
        {
            var billViewModel = (from b in _context.Bills
                                 join user in _context.Users on b.UserId equals user.UserId
                                 where b.BillId == billId
                                 select new BillViewModel
                                 {
                                     BillId = b.BillId,
                                     BillCode = b.BillCode,
                                     Email = b.Email,
                                     UserName = user.UserName,
                                     PhoneNumber = user.PhoneNumber,
                                     Total = b.Total,
                                     Promotion = b.Promotion,
                                     HistoricalCost = b.HistoricalCost,
                                     Status = b.Status,
                                     CreateAt = b.CreateAt.GetValueOrDefault(),
                                     ExpirationDate = b.ExpirationDate.GetValueOrDefault(),
                                     PaymentMethod = b.PaymentMethod,
                                     CardHolderName = b.CardHolderName,
                                     BillDetails = (from detail in _context.BillDetails
                                                    join course in _context.Courses on detail.CourseId equals course.CourseId
                                                    where detail.BillId == b.BillId
                                                    select new BillDetailViewModel
                                                    {
                                                        BillDetailId = detail.BillDetailId,
                                                        CourseId = course.CourseId,
                                                        Title = course.Title,
                                                        ImageCourseUrl = course.ImageCourseUrl,
                                                        Price = course.Price,
                                                        DiscountPrice = course.DiscountPrice,
                                                        // Các thuộc tính khác...
                                                    }).ToList()
                                 })
                                .FirstOrDefault();

            return billViewModel;
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
        public List<BillViewModel> GetAllBillsWithUser()
        {
            var billQuery = from bill in _context.Bills
                            join user in _context.Users on bill.UserId equals user.UserId
                            select new BillViewModel
                            {
                                BillId = bill.BillId,
                                BillCode = bill.BillCode,
                                Email = bill.Email,
                                UserName = user.UserName,
                                PhoneNumber = user.PhoneNumber,
                                Total = bill.Total,
                                Status = bill.Status,
                                CreateAt = bill.CreateAt.GetValueOrDefault(),
                                ExpirationDate = bill.ExpirationDate.GetValueOrDefault(),
                                PaymentMethod = bill.PaymentMethod,
                                Promotion = bill.Promotion,
                                HistoricalCost = bill.HistoricalCost,
                                CardHolderName = bill.CardHolderName,
                                // Các thuộc tính khác...
                                BillDetails = (from detail in _context.BillDetails
                                               join course in _context.Courses on detail.CourseId equals course.CourseId
                                               where detail.BillId == bill.BillId
                                               select new BillDetailViewModel
                                               {
                                                   BillDetailId = detail.BillDetailId,
                                                   CourseId = course.CourseId,
                                                   Title = course.Title,
                                                   ImageCourseUrl = course.ImageCourseUrl,
                                                   Price = course.Price,
                                                   DiscountPrice = course.DiscountPrice,
                                               }).ToList()
                            };

            var billsWithDetails = billQuery.ToList();
            return billsWithDetails;
        }

        public List<Bill> GetAllBills() => _context.Bills.ToList();

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

        public async Task<BillDetail> GetBillDetailByCourseAndUser(int courseId, int userId, Guid billId)
        {
            return await _context.BillDetails
                         .FirstOrDefaultAsync(bd => bd.CourseId == courseId &&
                                              bd.Bill.UserId == userId &&
                                              bd.BillId == billId &&
                                              bd.Bill.Status == "Pending");
        }
        public int GetBillCountForDate(DateTime date)
        {
            // Lấy ngày bắt đầu và kết thúc của ngày đang xét
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            // Đếm số lượng hóa đơn được tạo trong khoảng thời gian này
            return _context.Bills
                           .Count(b => b.CreateAt >= startDate && b.CreateAt < endDate);
        }

        public Bill FindBillByBillCode(string billCode)
        {
            return _context.Bills
                .Include(bd => bd.BillDetails)
                .ThenInclude(c => c.Course)
                .FirstOrDefault(bill => bill.BillCode == billCode);
        }

        public bool UpdateBillStatus(Bill bill)
        {
            Bill? b = _context.Bills.Find(bill.BillId);
            if (b != null)
            {
                _context.Entry(b).CurrentValues.SetValues(bill);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
