using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class FavListRepository: IFavListRepository
    {
        private DerekmodeWeLearnSystemContext _ctx;
        public FavListRepository(DerekmodeWeLearnSystemContext ctx) 
        {
            _ctx = ctx;
        }

        public FavList GetById(int Id)
        {
            FavList f = _ctx.FavLists.FirstOrDefault(x => x.FavId == Id);
            return f;
        }

        public List<FavList> GetAll() 
        {
            return _ctx.FavLists.ToList();
        }

        public bool Create(FavList favList)
        {
            _ctx.FavLists.Add(favList);
            _ctx.SaveChanges();
            return true;
        }

        public bool Update(FavList favList)
        {
            FavList f = _ctx.FavLists.FirstOrDefault(x => x.FavId == favList.FavId);
            if(f != null)
            {
                _ctx.Entry(f).CurrentValues.SetValues(favList);
                _ctx.SaveChanges();
            }
            return true;
        }

        public bool Delete(int Id)
        {
            FavList f = _ctx.FavLists.FirstOrDefault(x => x.FavId == Id);
            if(f != null)
            {
                _ctx.Remove(f);
                _ctx.SaveChanges();
            }
            return true;
        }



        public List<FavList> GetAllByUserId(int userid)
        {
            return _ctx.FavLists.Where(f => f.UserId == userid).Include("Course").ToList();
        }
    }
}
