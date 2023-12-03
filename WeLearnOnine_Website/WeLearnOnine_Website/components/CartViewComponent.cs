using Microsoft.AspNetCore.Mvc;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IBillRepository _billRepository;

        public CartViewComponent(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int userId = 4;
            var itemCount = 0;
            if (userId != null) {
                itemCount = await _billRepository.GetCartCountAsync(userId);
            } 
            return View(itemCount);
        }
    }
}
