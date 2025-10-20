using System.ComponentModel.DataAnnotations;
using ErpSystem.Domain.Common;

namespace ErpSystem.Domain.Entities;

public class Product : BaseEntity
{
    private Product()
    {
    }

    public Product(
        string name,
        string? sku,
        decimal price,
        int stockQuantity)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Sku = sku;
        Price = price;
        stockQuantity = stockQuantity;
        IsActive = true;
    }

    [Required, MaxLength(200)]
    public string Name { get; private set; } = string.Empty;
    [MaxLength(100)]
    public string? sku  { get; private set; }
    [MaxLength(1000)]
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public decimal? CostPrice  { get; private set; }
    public int StockQuantity { get; private set; }
    public int? MinimumStockLevel  { get; private set; }
    [MaxLength(50)]
    public string? Unit { get; private set; }
    public bool IsActive  { get; private set; }
    public Guid? CategoryId { get; private set; }
    [MaxLength(500)]
    public string? ImageUrl { get; private set; }
    
    // Regras de negocio

    public void UpdateDetails(
        string name,
        string? description,
        string? sku,
        string? unit,
        string? imageUrl)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Sku = sku;
        Unit = unit;
        ImageUrl = imageUrl;
    }

    public void UpdatePricing(decimal price, decimal? costPrice)
    {
        if (price < 0)
            throw new ArgumentException("Preço não pode ser negativo", nameof(price));
        
        if (costPrice.HasValue && costPrice.Value < 0)
            throw new ArgumentException("Preço de custo não pode ser negativo", nameof(costPrice));

        Price = price;
        CostPrice = costPrice;
    }

    public void UpdateStock(int quantity, int? minimumLevel = null)
    {
        if (quantity < 0)
            throw new ArgumentException("Quantidade em estoque não pode ser negativo", nameof(quantity));

        StockQuantity = quantity;

        if (minimumLevel.HasValue)
        {
            if (minimumLevel.Value < 0)
                throw new ArgumentException("Nível de estoque minimo não pode ser negativo", nameof(minimumLevel));

            MinimumStockLevel = minimumLevel;
        }
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantidade deve ser positiva", nameof(quantity));

        StockQuantity += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantidade deve ser positivo", nameof(quantity));
        
        if (StockQuantity < quantity)
            throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
        
        StockQuantity -= quantity.
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public bool IsLowStock() =>
        MinimumStockLevel.HasValue && StockQuantity <= MinimumStockLevel.Value;

    public void SetCategory(Guid? categoryId) => CategoryId = categoryId;
}