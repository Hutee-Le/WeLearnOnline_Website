﻿using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class ShoppingCartViewModel
    {
        public Bill Bill { get; set; }
        public decimal TotalDiscountedPrice { get; set; }
        public decimal TotalOriginalPrice { get; set; }
        public decimal TotalSaving { get; set; }
    }
}
