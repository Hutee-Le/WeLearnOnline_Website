using System;
using System.Collections.Generic;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class BillViewModel
    {
        public Guid BillId { get; set; }

        public string BillCode { get; set; }
        public string UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }
        public decimal? HistoricalCost { get; set; }
        public decimal? Promotion { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public string? CardHolderName { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        //public BillDetail? BillDetail { get; set; }
        //public Course? Course { get; set; }

        public List<BillDetailViewModel> BillDetails { get; set; }
    }
}
