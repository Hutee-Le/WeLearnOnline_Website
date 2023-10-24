using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class FavList
{
    public int FavId { get; set; }

    public int CourseId { get; set; }

    public int? UserId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User? User { get; set; }
}
