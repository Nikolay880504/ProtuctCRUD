
using System.ComponentModel.DataAnnotations;
using FIrstProductCRUD.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FIrstProductCRUD.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<WebSiteUser> _signInManager;
        private readonly UserManager<WebSiteUser> _userManager;
        private readonly IUserStore<WebSiteUser> _userStore;
        private readonly IUserEmailStore<WebSiteUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
       
        public RegisterModel(
            UserManager<WebSiteUser> userManager,
            IUserStore<WebSiteUser> userStore,
            SignInManager<WebSiteUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Почта")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Длина пароля должна быть не менее {2} и не более {1} символов.", MinimumLength = 6)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }


            [Display(Name = "Повторить пароль")]
            [Compare("Password", ErrorMessage = "Пароль и пароль подтверждения не совпадают.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }

        private WebSiteUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<WebSiteUser>();
            }
            catch
            {
                throw new InvalidOperationException($@"
                         Невозможно создать экземпляр '{nameof(IdentityUser)}'. 
                         Убедитесь, что '{nameof(IdentityUser)}' не является абстрактным классом и имеет 
                         конструктор без параметров, или, как альтернатива, 
                         переопределите страницу регистрации в /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<WebSiteUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException($@"Стандартный интерфейс пользователя требует хранилище пользователей
                                                с поддержкой электронной почты.");
            }
            return (IUserEmailStore<WebSiteUser>)_userStore;
        }
    }
}
