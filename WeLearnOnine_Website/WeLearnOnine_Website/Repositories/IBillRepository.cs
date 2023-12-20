using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.ViewModels;

namespace WeLearnOnine_Website.Repositories
{
    public interface IBillRepository
    {
        List<Bill> GetAllBills();
        List<BillViewModel> GetAllBillsWithUser();
        Bill GetPendingBillByUserId(int userId);
        BillViewModel GetBillViewModelById(Guid billId);
        Bill FindBillByBillCode(string billCode);
        Bill GetBillById(Guid billId);
        Task<int> GetCartCountAsync(int userId);

        Task<BillDetail> GetBillDetailByCourseAndUser(int courseId, int userId, Guid billId);

        void CreateBill(Bill bill); 

        void UpdateBill(Bill bill); 

        void DeleteBill(Guid billId);

        void AddBillDetail(BillDetail billDetail);
        void RemoveBillDetail(Guid billDetailId);

        int GetBillCountForDate(DateTime date);
        bool UpdateBillStatus(Bill bill);
    }
}
