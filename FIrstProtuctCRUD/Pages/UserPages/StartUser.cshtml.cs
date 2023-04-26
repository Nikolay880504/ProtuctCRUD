using FIrstProductCRUD.Data;
using FIrstProductCRUD.Models;
using FIrstProtuctCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIrstProductCRUD.Pages.UserPages
{
    public class StartUserModel : PageModel
    {
        private readonly IServiceCartStorage _serviceCartStorage;
        private readonly IServiceStorage _serviceStorage;
        public List<Product> AllProducts { get; set; }

        public StartUserModel(IServiceStorage serviceStorage, IServiceCartStorage serviceCartStorage)
        {
            _serviceStorage = serviceStorage;
            _serviceCartStorage = serviceCartStorage;
        }

        public IActionResult OnGet()
        {
            AllProducts = _serviceStorage.GetProducts();
            return Page();
        }
        public async Task<IActionResult> OnPostAddCart(CartProduct cartProduct)
        {
            cartProduct.UserId = Models.User.Id;
            _serviceCartStorage.AddCartProduct(cartProduct);
            return RedirectToPage("/UserPages/StartUser");
        }
    }
}
