using Microsoft.AspNetCore.Mvc;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

namespace WeLearnOnine_Website.components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IBillRepository _billRepository;
        private readonly Helper _helper;

        public CartViewComponent(IBillRepository billRepository, Helper helper)
        {
            _billRepository = billRepository;
            _helper = helper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsPrincipal = HttpContext.User;
            int userId = await _helper.GetUserId(claimsPrincipal);
            var itemCount = 0;
            if (userId != 0) {
                itemCount = await _billRepository.GetCartCountAsync(userId);
            } 
            return View(itemCount);
        }
    }
}
