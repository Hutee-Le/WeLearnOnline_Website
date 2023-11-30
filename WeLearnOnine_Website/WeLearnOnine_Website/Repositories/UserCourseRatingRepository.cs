using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Repositories
{
    public class UserCourseRatingRepository: IUserCourseRatingRepository
    {
        private DerekmodeWeLearnSystemContext _ctx;

        public UserCourseRatingRepository(DerekmodeWeLearnSystemContext ctx)
        {
            _ctx = ctx;
        }
        public UserCourseRating RatingCourse(UserCourseRating rating)
        {
            _ctx.UserCourseRatings.Add(rating);
            _ctx.SaveChanges();
            return rating;
        }

    }
}
