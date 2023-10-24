using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface ICommentRepository
    {
        public Comment GetById(int Id);
        public List<Comment> GetAll();
        public bool Create(Comment comment);
        public bool Update(Comment comment);
        public bool Delete(int Id);
    }
}
