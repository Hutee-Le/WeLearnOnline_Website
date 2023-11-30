using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class BillDetail
{
    public Guid BillDetailId { get; set; }

    public Guid BillId { get; set; }

    public int CourseId { get; set; }

    public decimal Price { get; set; }

    public DateTime Date { get; set; }

    public decimal? DiscountPrice { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
