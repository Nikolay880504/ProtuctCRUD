// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FIrstProductCRUD.Models;

namespace FIrstProductCRUD.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<WebSiteUser> _signInManager;//предоставляет методы для управления процессом аутентификации пользователей
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<WebSiteUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Запомнить меня?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))//определяет ошибку
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");//возвращает на последнюю страницу

            // Clear the existing external cookie to ensure a clean login process
            //IdentityConstants обычно определены различные константы, используемые в Identity Framework для настройки и работы с аутентификацией и авторизацией.
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);//вызывается для выхода пользователя из систему

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();// предоставляет пользователю возможность выбора провайдера аутентификации из списка доступных.

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                //позволяет аутентифицировать пользователя, проверяя его учетные данные (логин и пароль), 
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToPage("/ProducList", new { area = "User" });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            return Page();
        }
    }
}
