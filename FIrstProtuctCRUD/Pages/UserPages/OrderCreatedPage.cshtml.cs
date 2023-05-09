using FIrstProductCRUD.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FIrstProductCRUD.Pages.UserPages
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
