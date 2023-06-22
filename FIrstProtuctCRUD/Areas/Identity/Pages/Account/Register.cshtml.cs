// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using FIrstProductCRUD.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace FIrstProductCRUD.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<WebSiteUser> _signInManager;
        private readonly UserManager<WebSiteUser> _userManager;//представляет основной класс для управления пользователями.
        private readonly IUserStore<WebSiteUser> _userStore;//Интерфейс служит  для взаимодейстивя с БД 
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
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();// предоставляет пользователю возможность выбора провайдера аутентификации из списка доступных.
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
           // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();// предоставляет пользователю возможность выбора провайдера аутентификации из списка доступных.
            if (ModelState.IsValid)//Проверяет нет ошибок ввода
            {
                var user = CreateUser();//создаеться обьект IdentityUser
                
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);//используется для установки имени пользователя в БД.
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);//используется для установки почты пользователя  в БД.
                var result = await _userManager.CreateAsync(user, Input.Password);//создание нового пользователя в БД

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);//аутентификация пользователя и установки куки аутентификации.
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
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<WebSiteUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<WebSiteUser>)_userStore;
        }
    }
}
