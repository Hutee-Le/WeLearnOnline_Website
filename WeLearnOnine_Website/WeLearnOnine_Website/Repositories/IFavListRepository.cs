using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface IFavListRepository
    {
        public FavList GetById(int id);
        public List<FavList> GetAll();

        public List<FavList> GetAllByUserId(int userid);
        public bool Create(FavList favList);
        public bool Update(FavList favList);
        public bool Delete(int Id);
    }
}
