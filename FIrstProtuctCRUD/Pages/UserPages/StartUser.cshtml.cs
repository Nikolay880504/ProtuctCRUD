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
        [BindProperty]
        public CartProduct CartProduct { get; set; }

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
        public async Task<IActionResult> OnPostAddCart()
        {
           var product =  _serviceStorage.GetByIdOrNull(CartProduct.ProductId);
            if (product.Quantity < CartProduct.QuantityProducts)
            {
                ModelState.AddModelError("CartProduct.QuantityProducts", "“акого количества товара нет в наличии");
            }
            if (!ModelState.IsValid)
            {
                AllProducts = _serviceStorage.GetProducts();
                return Page();
            }
            CartProduct.UserId = Models.User.Id;
            _serviceCartStorage.AddCartProduct(CartProduct);
            return RedirectToPage("/UserPages/StartUser");
        }
    }
}
