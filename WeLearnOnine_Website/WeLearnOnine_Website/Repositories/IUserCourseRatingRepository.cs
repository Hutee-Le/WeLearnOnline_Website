using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public interface IUserCourseRatingRepository
    {
        public UserCourseRating RatingCourse(UserCourseRating rating);
    }
}
