namespace WeLearnOnine_Website.Models
{
    public interface ILessonRepository
    {
        //IEnumerable<Lesson> GetAll();

        List<Lesson> GetAllBaiHoc();
        public Lesson getLessonbyCourse(int courseid, int lessonid);
        void Add(Lesson lesson);
        void Update(Lesson lesson);
        void Delete(int id);
        void DeleteAll();
        Lesson GetById(int id);
    }
}
