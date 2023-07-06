using FIrstProductCRUD.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIrstProductCRUD.User.Pages
{

    public class OrderCreatedPageModel : PageModel
    {
        public string Id { get; set; }
        public void OnGet(string id)
        {
            Id = id;
        }
    }
}
