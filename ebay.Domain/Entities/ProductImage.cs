using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsPrimary { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public Product? Product { get; set; }
}
