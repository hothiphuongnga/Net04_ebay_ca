using System;
using System.Collections.Generic;

namespace ebay.Domain.Entities;

public class Product
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public int Stock { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool Deleted { get; private set; }

    private readonly List<Listing> _listings = new();
    public IReadOnlyCollection<Listing> Listings => _listings;

    private readonly List<OrderDetail> _orderDetails = new();
    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails;

    private readonly List<ProductImage> _productImages = new();
    public IReadOnlyCollection<ProductImage> ProductImages => _productImages;

    private readonly List<Rating> _ratings = new();
    public IReadOnlyCollection<Rating> Ratings => _ratings;

    // Constructor đảm bảo invariants
    public Product(string name, string description, decimal price, int stock)
    {
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        SetStock(stock);
        CreatedAt = DateTime.UtcNow;
        Deleted = false;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required");
        Name = name;
    }

    public void SetDescription(string description)
    {
        Description = description ?? string.Empty;
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be > 0");
        Price = price;
    }

    public void SetStock(int stock)
    {
        if (stock < 0)
            throw new ArgumentException("Stock cannot be negative");
        Stock = stock;
    }

    public void DecreaseStock(int quantity)
    {
        if (Stock < quantity)
            throw new InvalidOperationException("Insufficient stock");
        Stock -= quantity;
    }

    public void MarkDeleted()
    {
        Deleted = true;
    }
}
