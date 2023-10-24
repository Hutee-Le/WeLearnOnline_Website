using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Comment
{
    public int CmtId { get; set; }

    public string? StaffId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public string? Title { get; set; }

    public string ContentNote { get; set; } = null!;

    public DateTime? Date { get; set; }

    public int? ReplyId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Staff? Staff { get; set; }

    public virtual User User { get; set; } = null!;
}
