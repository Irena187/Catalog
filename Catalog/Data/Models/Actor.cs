using System;
using System.Collections.Generic;

namespace Catalog.Data.Models;

public partial class Actor
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? Nationality { get; set; }

    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();
}
