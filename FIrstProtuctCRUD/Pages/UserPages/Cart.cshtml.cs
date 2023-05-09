using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FIrstProductCRUD.Models;
using FIrstProductCRUD.Data;

namespace FIrstProductCRUD.Pages.UserPages
{
    public class CartModel : PageModel
    {
        private readonly IServiceCartStorage _serviceCartProduct;
        private readonly IServiceOrderStorage _serviceOrderStorage;
        private readonly IServiceStorage _serviceStorage;

        public List<CartProduct> UserCart { get; set; }
        public CartModel(IServiceCartStorage serviceCartStorage, IServiceOrderStorage serviceOrderStorage, IServiceStorage  serviceStorage)
        {
            this._serviceCartProduct = serviceCartStorage;
            this._serviceStorage = serviceStorage;
            this._serviceOrderStorage = serviceOrderStorage;
        }

        public IActionResult OnGet()
        {
            UserCart = _serviceCartProduct.GetCartProductForUser(Models.User.Id);
            return Page();
        }
        public async Task<IActionResult> OnPostRemove(int productIdFromCart)
        {
            _serviceCartProduct.RemoveProductFromCart(productIdFromCart);
            return RedirectToPage("/UserPages/Cart");
        }
        public async Task<IActionResult> OnPostAddOrder()
        {
            var order = _serviceOrderStorage.AddOrder(Models.User.Id);
            _serviceStorage.ChangeQuantityProducts(order);
            _serviceCartProduct.RemoveCart(Models.User.Id);
            return RedirectToPage("/UserPages/OrderCreatedPage", new { id = order.OrderId.ToString()});
        }
    }
}
