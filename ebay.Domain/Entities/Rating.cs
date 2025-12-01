using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class Rating
{
    public int Id { get; set; }

    public int RaterId { get; set; }

    public int RatedUserId { get; set; }

    public int? ProductId { get; set; }

    public int RatingScore { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public Product? Product { get; set; }

    public User RatedUser { get; set; } = null!;

    public User Rater { get; set; } = null!;
}
