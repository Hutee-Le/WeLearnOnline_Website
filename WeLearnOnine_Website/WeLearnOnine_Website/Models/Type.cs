using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Type
{
    public int TypeId { get; set; }

    public string Title { get; set; } = null!;

    public string? TypeDescription { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
