using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Catalog.Data.Models;

namespace Catalog.Pages_FilmGenre
{
    public class IndexModel : PageModel
    {
        private readonly Catalog.Data.Models.CatalogContext _context;

        public IndexModel(Catalog.Data.Models.CatalogContext context)
        {
            _context = context;
        }

        public IList<Film> Film { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Film = await _context.Films.ToListAsync();
        }
    }
}
