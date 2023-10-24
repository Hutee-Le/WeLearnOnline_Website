using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int CourseId { get; set; }

    public int TypeId { get; set; }

    public int? Stt { get; set; }

    public string Title { get; set; } = null!;

    public string UrlVideo { get; set; } = null!;

    public string? ImageLessonUrl { get; set; }

    public int? Time { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Type Type { get; set; } = null!;

    public virtual ICollection<UserLesson> UserLessons { get; set; } = new List<UserLesson>();
}
