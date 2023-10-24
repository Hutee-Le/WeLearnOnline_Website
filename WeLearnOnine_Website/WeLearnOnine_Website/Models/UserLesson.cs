using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class UserLesson
{
    public int Ulid { get; set; }

    public int UserId { get; set; }

    public int LessonId { get; set; }

    public bool Status { get; set; }

    public int? Time { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
