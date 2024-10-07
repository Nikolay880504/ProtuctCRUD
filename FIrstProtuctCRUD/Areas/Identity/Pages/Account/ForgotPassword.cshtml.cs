
using System.ComponentModel.DataAnnotations;
using FIrstProductCRUD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIrstProductCRUD.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<WebSiteUser> _userManager;
 

        public ForgotPasswordModel(UserManager<WebSiteUser> userManager)
        {
            _userManager = userManager;        
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Введите адрес электронной почты.")]
            [EmailAddress]           
            public string Email { get; set; }
          
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.FindByEmailAsync(Input.Email);
               
                if(user != null)
                {
                    return RedirectToPage("./ResetPassword");
                }       

            return Page();
        }
    }
}
