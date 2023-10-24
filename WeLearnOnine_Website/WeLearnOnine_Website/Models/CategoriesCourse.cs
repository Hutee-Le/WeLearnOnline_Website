using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class CategoriesCourse
{
    public int CatCourseId { get; set; }

    public int CategoriesId { get; set; }

    public int CourseId { get; set; }

    public string? Topic { get; set; }

    public virtual Category Categories { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
