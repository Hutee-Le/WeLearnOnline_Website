using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class UserCourseRating
{
    public int Ucrid { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public double? Rating { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
