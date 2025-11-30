namespace FoodStock.Inventory.Domain.Model.Aggregates;

public partial class Product
{
    public long Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public decimal Quantity { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public bool Deleted { get; private set; }

    public decimal TotalPrice => UnitPrice * Quantity;

    protected Product() { }

    public Product(string name, decimal unitPrice, decimal quantity, DateTime? expirationDate, string category)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice));
        if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));

        Name = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
        ExpirationDate = expirationDate;
        Category = category ?? string.Empty;
        Deleted = false;
    }

    public void Edit(string name, decimal unitPrice, decimal quantity, DateTime? expirationDate, string category)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice));
        if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));

        Name = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
        ExpirationDate = expirationDate;
        Category = category ?? string.Empty;
    }

    public void Consume(decimal amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Consume amount must be > 0");
        if (Quantity < amount) throw new InvalidOperationException($"Insufficient stock for product {Name}");
        Quantity -= amount;
    }

    public void Delete() => Deleted = true;
}
