using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class HistoryPayment
{
    public int HistoryPaymentId { get; set; }

    public int UserId { get; set; }

    public Guid BillId { get; set; }

    public int CourseId { get; set; }

    public DateTime? Date { get; set; }

    public string? Status { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
