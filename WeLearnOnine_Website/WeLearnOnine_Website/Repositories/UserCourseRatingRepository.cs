using Microsoft.EntityFrameworkCore;
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

        //public UserCourseRating GetRatingByUser(int courseid)
        //{
        //    var userCourseRating = _ctx.UserCourseRatings
        //                      .Where(r => r.CourseId == courseid)
        //                      .ToList();

        //    return userCourseRating;
        //}
        public List<UserCourseRating> GetRatingsForCourse(int courseId)
        {
            List<UserCourseRating> courseRatings = _ctx.UserCourseRatings
                                    .Where(r => r.CourseId == courseId)
                                    .ToList();

            return courseRatings;
        }
        public UserCourseRating RatingCourse(UserCourseRating rating)
        {
            _ctx.UserCourseRatings.Add(rating);
            _ctx.SaveChanges();
            return rating;
        }

        //UserCourseRating IUserCourseRatingRepository.GetRatingsForCourse(int courseid)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
