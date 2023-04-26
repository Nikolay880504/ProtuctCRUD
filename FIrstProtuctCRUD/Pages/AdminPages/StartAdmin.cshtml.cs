using FIrstProductCRUD.Data;
using FIrstProtuctCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIrstProductCRUD.Pages.AdminPages
{
    public class StartAdmin : PageModel
    {
        private readonly IServiceStorage _ServiceStorage;
        public List<Product> AllProducts { get; set; }

        public StartAdmin(IServiceStorage serviceStorage)
        {
            _ServiceStorage = serviceStorage;
        }

        public IActionResult OnGet()
        {
            AllProducts = _ServiceStorage.GetProducts();
            return Page();
        }
        public async Task<IActionResult> OnPostRemove(int productId)
        {
            _ServiceStorage.RemoveProduct(productId);
            return RedirectToPage("/AdminPages/StartAdmin");
        }
    }
}
