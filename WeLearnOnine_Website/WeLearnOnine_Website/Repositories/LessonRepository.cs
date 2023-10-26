using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
	public class LessonRepository : ILessonRepository
	{
		public DerekmodeWeLearnSystemContext _context;

		public LessonRepository(DerekmodeWeLearnSystemContext context)
		{
			_context = context;
		}

        public Lesson GetById(int id)
		{
			return _context.Lessons.FirstOrDefault(p => p.LessonId == id);
		}
		public void Add(Lesson lesson)
		{
			if (lesson == null)
				throw new ArgumentNullException(nameof(lesson));

			_context.Lessons.Add(lesson);
			_context.SaveChanges();
		}

		public void Update(Lesson lesson)
		{
			if (lesson == null)
				throw new ArgumentNullException(nameof(lesson));

			_context.Lessons.Update(lesson);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var lesson = _context.Lessons.FirstOrDefault(p => p.LessonId == id);
			if (lesson != null)
			{
				_context.Lessons.Remove(lesson);
				_context.SaveChanges();
			}
		}
		public void DeleteAll()
		{
			throw new NotImplementedException();
		}

        public List<Lesson> GetAllBaiHoc()
        {
            return _context.Lessons.ToList();
        }
    }
}
