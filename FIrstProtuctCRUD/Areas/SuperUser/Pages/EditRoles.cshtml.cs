using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FIrstProductCRUD.Models;
using FIrstProductCRUD.Areas.SuperUser.ModelForChangeRole;

namespace FIrstProductCRUD.Areas.SuperUser.Pages
{
    public class EditRolesModel : PageModel
    {
        private readonly UserManager<WebSiteUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public ChangeRoleViewModel ChangeRoleViewModel { get; set; }
        public EditRolesModel(UserManager<WebSiteUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task OnGet(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();

                ChangeRoleViewModel = new ChangeRoleViewModel
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
            }
        }

        public async Task<IActionResult> OnPostEdit(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addRoles = roles.Except(userRoles);
                var deleteRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addRoles);
                await _userManager.RemoveFromRolesAsync(user, deleteRoles);

                return Redirect("./ListUser");
            }
            return NotFound();
        }
    }
}
