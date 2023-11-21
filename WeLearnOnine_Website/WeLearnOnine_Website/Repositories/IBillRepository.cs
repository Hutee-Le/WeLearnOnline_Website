using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface IBillRepository
    {
        Bill GetPendingBillByUserId(int userId);

        Bill GetBillById(Guid billId); 

        void CreateBill(Bill bill); 

        void UpdateBill(Bill bill); 

        void DeleteBill(Guid billId);

        void AddBillDetail(BillDetail billDetail);
        void RemoveBillDetail(Guid billDetailId);
    }
}
