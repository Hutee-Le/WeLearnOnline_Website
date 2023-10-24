using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class UserCourse
{
    public int Ucid { get; set; }

    public string UserId { get; set; } = null!;

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
