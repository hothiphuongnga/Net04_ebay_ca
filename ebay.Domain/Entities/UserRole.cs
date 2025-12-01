using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class UserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string? Description { get; set; }

    public Role Role { get; set; } = null!;

    public User User { get; set; } = null!;
}
