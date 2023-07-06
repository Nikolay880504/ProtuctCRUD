using Microsoft.AspNetCore.Identity;

namespace FIrstProductCRUD.Areas.SuperUser.ModelForChangeRole
{
    public class ChangeRoleViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<IdentityRole<int>> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }

        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole<int>>();
            UserRoles = new List<string>();
        }

    }
}
