using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface IBillRepository
    {
        List<Bill> GetAllBills();
        IEnumerable<Bill> GetAllBillsWithUser();
        Bill GetPendingBillByUserId(int userId);

        Bill GetBillById(Guid billId);
        Task<int> GetCartCountAsync(int userId);

        Task<BillDetail> GetBillDetailByCourseAndUser(int courseId, int userId, Guid billId);

        void CreateBill(Bill bill); 

        void UpdateBill(Bill bill); 

        void DeleteBill(Guid billId);

        void AddBillDetail(BillDetail billDetail);
        void RemoveBillDetail(Guid billDetailId);

    }
}
