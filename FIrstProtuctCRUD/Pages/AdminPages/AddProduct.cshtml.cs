using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FIrstProductCRUD.Models;
using FIrstProductCRUD.Data;
using Microsoft.Graph;
using System;
using FIrstProtuctCRUD.Models;
using System.Security.Cryptography;

namespace FIrstProtuctCRUD.Pages.ProductsPage
{
    public class AddProductModel : PageModel
    {
        private readonly IServiceStorage _ServiceStorage;
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        [BindProperty]
        public ViewModel ViewModel { get; set; }


        public AddProductModel(IServiceStorage serviceStorage)
        {
            _ServiceStorage = serviceStorage;
        }
        public void OnGet()
        {
            Message = "Добавьте ваш товар";
            ViewModel = new ViewModel();
            if (Id.HasValue)
            {
                Product product = _ServiceStorage.GetByIdOrNull(Id.Value);
                ViewModel.Name = product.Name;
                ViewModel.Price = product.Price;
                ViewModel.Code = product.Code;
                ViewModel.Quantity = product.Quantity;
            }
        }
        public async Task<IActionResult> OnPost()
        {
            if (_ServiceStorage.IsCodeExists(ViewModel.Code, Id))
            {
                ModelState.AddModelError("ViewModel.Code", "Товар с таким кодом уже существует");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Product product = new Product();
            product.Name = ViewModel.Name;
            product.Price = ViewModel.Price;
            product.Code = ViewModel.Code;
            product.Quantity = ViewModel.Quantity;
            if (Id.HasValue)
            {
                product.Id = Id.Value;
                _ServiceStorage.Update(product);
            }
            else
            {
                _ServiceStorage.AddProduct(product);
            }

            return RedirectToPage("/AdminPages/StartAdmin");
        }
    }
}
