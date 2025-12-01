using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class Bid
{
    public int Id { get; set; }

    public int ListingId { get; set; }

    public int BidderId { get; set; }

    public decimal BidAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public User Bidder { get; set; } = null!;

    public Listing Listing { get; set; } = null!;
}
