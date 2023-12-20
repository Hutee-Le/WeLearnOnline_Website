using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class ShoppingCartViewModel
    {
        public Bill Bill { get; set; }
        public decimal TotalDiscountedPrice { get; set; }
        public decimal TotalOriginalPrice { get; set; }
        public decimal TotalSaving { get; set; }
        public string SelectedPaymentMethod { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
