using Catalog.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Pages_Genre
{
    public class IndexModel : PageModel
    {
        private readonly CatalogContext _context;

        public IndexModel(CatalogContext context)
        {
            _context = context;
        }

        public IList<Genre> Genres { get; set; } = new List<Genre>();

        public async Task OnGetAsync()
        {
            Genres = await _context.Genres.ToListAsync();
        }
    }
}
