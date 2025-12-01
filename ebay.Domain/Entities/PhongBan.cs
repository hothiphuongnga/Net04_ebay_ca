using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class PhongBan
{
    public int MaPb { get; set; }

    public string? TenPhongBan { get; set; }

    public string? DiaDiem { get; set; }

    public ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
