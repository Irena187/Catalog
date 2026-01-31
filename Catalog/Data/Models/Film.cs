using System;
using System.Collections.Generic;

namespace Catalog.Data.Models;

public partial class Film
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Year { get; set; }

    public string? Director { get; set; }

    public string? Description { get; set; }

    public int? Rating { get; set; }

    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    public virtual ICollection<FilmGenre> FilmGenres { get; set; } = new List<FilmGenre>();
}
