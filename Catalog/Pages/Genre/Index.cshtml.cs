using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Catalog.Data.Models;

namespace Catalog.Pages_Genre
{
    public class IndexModel : PageModel
    {
        private readonly CatalogContext _context;

        public IndexModel(CatalogContext context)
        {
            _context = context;
        }

        public IList<Genre> Genre { get; set; } = new List<Genre>();

        public async Task OnGetAsync()
        {
            Genre = await _context.Genres.ToListAsync();
        }
    }
}
