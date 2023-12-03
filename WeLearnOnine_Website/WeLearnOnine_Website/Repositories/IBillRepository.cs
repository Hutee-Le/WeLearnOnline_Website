using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface IBillRepository
    {
        //List<Bill> GetAllBill();
        IEnumerable<Bill> GetAllBillsWithUser();
        Bill GetPendingBillByUserId(int userId);

        Bill GetBillById(Guid billId);
        Task<int> GetCartCountAsync(int userId);

        void CreateBill(Bill bill); 

        void UpdateBill(Bill bill); 

        void DeleteBill(Guid billId);

        void AddBillDetail(BillDetail billDetail);
        void RemoveBillDetail(Guid billDetailId);
    }
}
