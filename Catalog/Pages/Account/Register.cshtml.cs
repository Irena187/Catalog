using Catalog.Data.Models;
using Catalog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Catalog.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<Userr> _userManager;
        private readonly SignInManager<Userr> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<Userr> userManager,
            SignInManager<Userr> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }


        // Добави това за dropdown
        public List<string> AvailableRoles { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            // Покажи само User и Manager (НЕ Admin за сигурност!)
            AvailableRoles = new List<string> { "User", "Manager" };
        }

       

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new Userr
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Потребител създаден с парола.");
                    /*
                                        // Добави роля "User" по подразбиране
                                        await _userManager.AddToRoleAsync(user, "User");

                                        // Влез автоматично след регистрация
                                        await _signInManager.SignInAsync(user, isPersistent: false);
                    */
                    await _userManager.AddToRoleAsync(user, Input.Role);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // Зареди отново ролите ако има грешка
            AvailableRoles = new List<string> { "User", "Manager" };
            // Ако стигнем до тук, нещо не е наред, покажи формата отново
            return Page();
        }
    }
}