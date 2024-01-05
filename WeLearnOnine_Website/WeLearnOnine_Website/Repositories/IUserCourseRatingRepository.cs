using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface IUserCourseRatingRepository
    {
        public UserCourseRating RatingCourse(UserCourseRating rating);
        //public UserCourseRating GetRatingsForCourse(int courseid);
        public List<UserCourseRating> GetRatingsForCourse(int courseId);
    }
}
