using System;
using System.Collections.Generic;

namespace Catalog.Data.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<FilmGenre> FilmGenres { get; set; } = new List<FilmGenre>();
}
