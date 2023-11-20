using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface IBillRepository
    {
        Bill GetPendingBillByUserId(int userId);
    }
}
