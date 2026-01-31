using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Catalog.Data.Models;
using System.Threading.Tasks;

namespace Catalog.Pages_Actor
{
    public class DetailsModel : PageModel
    {
        private readonly CatalogContext _context;

        public DetailsModel(CatalogContext context)
        {
            _context = context;
        }

        public Actor Actor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Actor = await _context.Actors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Actor == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
