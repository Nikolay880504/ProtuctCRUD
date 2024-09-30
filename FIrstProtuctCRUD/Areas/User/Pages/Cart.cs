using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FIrstProductCRUD.Models;
using FIrstProductCRUD.Data;
using FIrstProductCRUD.Extensions;



namespace FIrstProductCRUD.User.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceCartStorage _serviceCartProduct;
        private readonly IServiceOrderStorage _serviceOrderStorage;
        private readonly IServiceStorage _serviceStorage;

        public List<CartProduct> UserCart { get; set; }
        public CartModel(IServiceCartStorage serviceCartStorage, IServiceOrderStorage serviceOrderStorage, IServiceStorage serviceStorage)
        {
            _serviceCartProduct = serviceCartStorage;
            _serviceStorage = serviceStorage;
            _serviceOrderStorage = serviceOrderStorage;

        }

        public IActionResult OnGet()
        {

            UserCart = _serviceCartProduct.GetCartProductForUser(HttpContext.GetUserIdOrDefault());
            return Page();
        }
        public async Task<IActionResult> OnPostRemove(int productIdFromCart)
        {
            _serviceCartProduct.RemoveProductFromCart(productIdFromCart);
            return RedirectToPage("/Cart", new { area = "User" });
        }
        public async Task<IActionResult> OnPostAddOrder()
        {
            var order = _serviceOrderStorage.AddOrder(HttpContext.GetUserIdOrDefault());
            _serviceStorage.ChangeQuantityProducts(order);
            _serviceCartProduct.RemoveCart(HttpContext.GetUserIdOrDefault());
            return RedirectToPage("./OrderCreatedPage", new { id = order.OrderId.ToString()});
        }


    }
}
