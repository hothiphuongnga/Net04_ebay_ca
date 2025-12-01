using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class Client
{
    public string ClientId { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string? ClientType { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
