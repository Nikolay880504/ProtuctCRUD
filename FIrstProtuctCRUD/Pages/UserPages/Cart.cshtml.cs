using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FIrstProductCRUD.Models;
using FIrstProductCRUD.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Graph;
using FIrstProductCRUD.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FIrstProductCRUD.Pages.UserPages
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly IServiceCartStorage _serviceCartProduct;
        private readonly IServiceOrderStorage _serviceOrderStorage;
        private readonly IServiceStorage _serviceStorage;
      
        public List<CartProduct> UserCart { get; set; }
        public CartModel(IServiceCartStorage serviceCartStorage, IServiceOrderStorage serviceOrderStorage, IServiceStorage serviceStorage)
        {
            this._serviceCartProduct = serviceCartStorage;
            this._serviceStorage = serviceStorage;
            this._serviceOrderStorage = serviceOrderStorage;
            
        }
       
        public IActionResult OnGet()
        {
           
            UserCart = _serviceCartProduct.GetCartProductForUser(HttpContext.GetUserIdOrDefault());
            return Page();
        }
        public async Task<IActionResult> OnPostRemove(int productIdFromCart)
        {
            _serviceCartProduct.RemoveProductFromCart(productIdFromCart);
            return RedirectToPage("/UserPages/Cart");
        }
        public async Task<IActionResult> OnPostAddOrder()
        {
            var order = _serviceOrderStorage.AddOrder(HttpContext.GetUserIdOrDefault());
            _serviceStorage.ChangeQuantityProducts(order);
            _serviceCartProduct.RemoveCart(HttpContext.GetUserIdOrDefault());
            return RedirectToPage("/UserPages/OrderCreatedPage", new { id = order.OrderId.ToString() });
        }

      
    }
}
