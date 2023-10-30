using Type = WeLearnOnine_Website.Models.Type;
namespace WeLearnOnine_Website.Repositories

{
    public interface ITypeRepository
    {
        List<Type> GetAll();
        Type GetById(int id);
        void Add(Type type);
        void Update(Type type);
        void Delete(int id);
    }
}
