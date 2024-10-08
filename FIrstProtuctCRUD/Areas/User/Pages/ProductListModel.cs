using FIrstProductCRUD.Data;
using FIrstProductCRUD.Extensions;
using FIrstProductCRUD.Models;
using FIrstProtuctCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIrstProductCRUD.User.Pages
{
    public class ProductListModel : PageModel
    {
        private readonly IServiceCartStorage _serviceCartStorage;
        private readonly IServiceStorage _serviceStorage;
        [BindProperty]
        public CartProduct CartProduct { get; set; }

        public List<Product> AllProducts { get; set; }

        public ProductListModel(IServiceStorage serviceStorage, IServiceCartStorage serviceCartStorage)
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
                ModelState.AddModelError(string.Empty, "����� �������� ������� ��������� ��������������.");
            }

            var product = _serviceStorage.GetByIdOrNull(CartProduct.ProductId);
            if (product == null)
            {
                ModelState.AddModelError(string.Empty, "������� �� ������.");
            }
            else if (product.Quantity < CartProduct.QuantityProducts)
            {
                ModelState.AddModelError("CartProduct.QuantityProducts", "������ ���������� ������ ��� � �������.");
            }

            if (!ModelState.IsValid)
            {
                AllProducts = _serviceStorage.GetProducts();
                return Page();
            }

            CartProduct.UserId = HttpContext.GetUserIdOrDefault();
            _serviceCartStorage.AddCartProduct(CartProduct);

            return RedirectToPage();

        }
    }
}
