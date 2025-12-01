using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public Order Order { get; set; } = null!;

    public Product Product { get; set; } = null!;
}
