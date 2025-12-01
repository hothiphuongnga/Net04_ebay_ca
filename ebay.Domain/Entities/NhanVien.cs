using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class NhanVien
{
    public int MaNhanVien { get; set; }

    public string? TenNhanVien { get; set; }

    public decimal? Luong { get; set; }

    public int? PhongBan { get; set; }

    public string? GioiTinh { get; set; }

    public PhongBan? PhongBanNavigation { get; set; }
}
