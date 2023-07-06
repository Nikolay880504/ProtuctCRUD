using FIrstProductCRUD.Constants;
using FIrstProductCRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FIrstProductCRUD.Areas.SuperUser.Pages
{
    [Authorize(Roles = RoleNameConstants.SuperUser)]
    public class ListUserModel : PageModel
    {
        private readonly UserManager<WebSiteUser> _userManager;
        public List<WebSiteUser> AllUsers { get; set; }
        public ListUserModel(UserManager<WebSiteUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            AllUsers = _userManager.Users.ToList();
            return Page();
        }
    }
}
