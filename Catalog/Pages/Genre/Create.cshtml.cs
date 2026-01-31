using Catalog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Pages_Genre
{
    [Authorize(Roles = "Manager,Admin")]
    public class CreateModel : PageModel
    {
        private readonly CatalogContext _context;

        public CreateModel(CatalogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Genre Genre { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var maxId = await _context.Genres.MaxAsync(g => (int?)g.Id) ?? 0;
            Genre.Id = maxId + 1;

            _context.Genres.Add(Genre);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
