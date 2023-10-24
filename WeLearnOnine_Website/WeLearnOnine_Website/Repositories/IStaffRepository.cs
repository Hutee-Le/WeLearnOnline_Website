namespace WeLearnOnine_Website.Models
{
    public interface IStaffRepository
    {
        bool Add(Staff staff);
        bool Update(Staff staff);
        bool Delete(string id);
        void DeleteAll();   
        //Course FindStaffByID(string id);
        List<Staff> GetAllStaffs();
    }
}
