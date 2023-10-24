using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Level
{
    public int LevelId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
