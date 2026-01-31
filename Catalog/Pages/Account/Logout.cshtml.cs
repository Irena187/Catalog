using Catalog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Catalog.Pages.Account
{
    public class LogoutModel : PageModel
    {
        /*   private readonly SignInManager<Userr> _signInManager;
           private readonly ILogger<LogoutModel> _logger;

           public LogoutModel(
               SignInManager<Userr> signInManager,
               ILogger<LogoutModel> logger)
           {
               _signInManager = signInManager;
               _logger = logger;
           }

           public async Task<IActionResult> OnPost(string returnUrl = null)
           {
               await _signInManager.SignOutAsync();
               _logger.LogInformation("Потребителят излезе.");

               if (returnUrl != null)
               {
                   return LocalRedirect(returnUrl);
               }
               else
               {
                   return RedirectToPage("/Index");
               }
           }
        */
        private readonly SignInManager<Userr> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(
            SignInManager<Userr> signInManager,
            ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        // Добави OnGet метод
        public async Task<IActionResult> OnGetAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Потребителят излезе.");
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Потребителят излезе.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}