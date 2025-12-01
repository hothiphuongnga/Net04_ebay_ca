using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class Listing
{
    public int Id { get; set; }

    public int SellerId { get; set; }

    public int? CategoryId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal? StartingPrice { get; set; }

    public decimal? CurrentPrice { get; set; }

    public bool? IsAuction { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public int? ProductId { get; set; }

    public ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public Category? Category { get; set; }

    public Product? Product { get; set; }

    public User Seller { get; set; } = null!;
}
