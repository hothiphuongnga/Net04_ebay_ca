using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public ICollection<Listing> Listings { get; set; } = new List<Listing>();

    public ICollection<LoginLog> LoginLogs { get; set; } = new List<LoginLog>();

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Rating> RatingRatedUsers { get; set; } = new List<Rating>();

    public ICollection<Rating> RatingRaters { get; set; } = new List<Rating>();

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
