namespace WeLearnOnine_Website.Models
{
    public interface ISkillRepository
    {
        bool Add(Staff staff);
        bool Update(Staff staff);
        bool Delete(string id);
        void DeleteAll();   
        List<Staff> GetAllStaffs();
        Staff FindStaffByID(string id);
        List<Staff> Search(string keyword);
    }
}
