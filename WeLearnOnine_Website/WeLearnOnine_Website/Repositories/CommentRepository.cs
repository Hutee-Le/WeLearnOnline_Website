using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private DerekmodeWeLearnSystemContext _ctx;

        public CommentRepository(DerekmodeWeLearnSystemContext ctx)
        {
            _ctx = ctx;
        }
        public Comment AddComment(Comment comment)
        {
            _ctx.Comments.Add(comment);
            _ctx.SaveChanges();
            return comment;
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

        public List<Comment> GetById(int Id)
        {
            List<Comment> c = _ctx.Comments.Include(c => c.User).Include(c => c.Staff).Where(x => x.CourseId == Id).ToList();
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
