using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
