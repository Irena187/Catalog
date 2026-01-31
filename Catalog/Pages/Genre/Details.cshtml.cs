using Catalog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Catalog.Pages_Genre
{
    public class DetailsModel : PageModel
    {
        private readonly CatalogContext _context;

        public DetailsModel(CatalogContext context)
        {
            _context = context;
        }

        public Genre Genre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (Genre == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
