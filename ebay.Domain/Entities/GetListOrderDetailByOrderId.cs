using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class GetListOrderDetailByOrderId
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? OrderDetail { get; set; }
}
