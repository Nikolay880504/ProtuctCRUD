using FIrstProductCRUD.Data;
using FIrstProductCRUD.Extensions;
using FIrstProductCRUD.Models;
using FIrstProtuctCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;

namespace FIrstProductCRUD.User.Pages
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
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError(string.Empty, "Чтобы добавить продукт требуется аутентификация.");

            }               
            var product =  _serviceStorage.GetByIdOrNull(CartProduct.ProductId);
            if (product.Quantity < CartProduct.QuantityProducts)
            {
                ModelState.AddModelError("CartProduct.QuantityProducts", "Такого количества товара нет в наличии");
            }
            if (!ModelState.IsValid)
            {
                AllProducts = _serviceStorage.GetProducts();
                return Page();
            }
            CartProduct.UserId = HttpContext.GetUserIdOrDefault();
            _serviceCartStorage.AddCartProduct(CartProduct);
            return RedirectToPage("./StartUser");
        }
    }
}
