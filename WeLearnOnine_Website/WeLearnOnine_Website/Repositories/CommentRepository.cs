using System.Diagnostics;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private StWelearnContext _ctx;

        public CommentRepository(StWelearnContext ctx)
        {
            _ctx = ctx;
        }

        public bool Create(Comment comment)
        {
            _ctx.Comments.Add(comment);
            _ctx.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            Comment c = _ctx.Comments.FirstOrDefault(x => x.CmtId == Id);
            if (c != null)
            {
                _ctx.Remove(c);
                _ctx.SaveChanges();
            }
            return true;
        }

        public List<Comment> GetAll()
        {
            return _ctx.Comments.ToList();
        }

        public Comment GetById(int Id)
        {
            Comment c = _ctx.Comments.FirstOrDefault(x => x.CmtId == Id);
            return c;
        }

        public bool Update(Comment comment)
        {
            Comment c = _ctx.Comments.FirstOrDefault(x => x.CmtId == comment.CmtId);
            if (c != null)
            {
                _ctx.Entry(c).CurrentValues.SetValues(comment);
                _ctx.SaveChanges();
            }
            return true;
        }
    }
}
