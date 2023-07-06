using FIrstProductCRUD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIrstProductCRUD.Areas.SuperUser.Pages
{
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<WebSiteUser> _userManager;

        public DeleteUserModel(UserManager<WebSiteUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string id )
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);

            if(result.Succeeded)
            {
                return Redirect("./ListUser");
            }       
            return NotFound();
        }
    }
}
