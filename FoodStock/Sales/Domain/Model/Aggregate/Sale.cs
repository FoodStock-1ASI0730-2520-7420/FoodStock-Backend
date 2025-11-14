using FoodStock.Sales.Domain.Model.Commands;
using FoodStock.Sales.Domain.Model.ValueObjects;

namespace FoodStock.Sales.Domain.Model.Aggregate;

public partial class Sale
{
    public Sale (){}
    public Sale(ESaleType saleType, EPaymentMethod paymentMethod, string waiter, List<SaleItem> saleItems)
    {
        SaleType = saleType;
        PaymentMethod = paymentMethod;
        Waiter = waiter;
        if (saleItems != null)
            _saleItems.AddRange(saleItems);
        Total = _saleItems.Sum(i => i.PriceUnit * i.Quantity);
    }

    public Sale(CreateSaleCommand command, List<SaleItem> saleItems)
        : this(command.SaleType, command.PaymentMethod, command.Waiter, saleItems)
    {
    }

    public int Id { get; }
    public ESaleType SaleType { get; private set; }
    public EPaymentMethod PaymentMethod { get; private set; }
    public decimal Total { get; private set; }
    public string Waiter { get; private set; }
    private readonly List<SaleItem> _saleItems = new();
    public IReadOnlyList<SaleItem> SaleItems => _saleItems.AsReadOnly();

    public void AddSaleItem(SaleItem item)
    {
        _saleItems.Add(item);
        Total = _saleItems.Sum(i => i.PriceUnit * i.Quantity);
    }
}