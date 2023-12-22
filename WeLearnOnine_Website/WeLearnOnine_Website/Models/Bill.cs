using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Bill
{
    public Guid BillId { get; set; }

    public int? UserId { get; set; }

    public string? BillCode { get; set; }

    public decimal Total { get; set; }

    public decimal? HistoricalCost { get; set; }

    public decimal? Promotion { get; set; }

    public string? Email { get; set; }

    public DateTime? CreateAt { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? PayType { get; set; }

    public string? CardHolderName { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public string? Status { get; set; }

    public string? PayUrl { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual ICollection<HistoryPayment> HistoryPayments { get; set; } = new List<HistoryPayment>();
}
