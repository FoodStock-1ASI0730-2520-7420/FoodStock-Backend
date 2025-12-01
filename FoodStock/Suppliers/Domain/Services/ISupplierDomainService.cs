using FoodStock.Suppliers.Domain.Model.Aggregate;

namespace FoodStock.Suppliers.Domain.Services;

public interface ISupplierDomainService
{
    bool IsValidEmail(string email);
    bool IsDuplicate(Supplier supplier, IEnumerable<Supplier> existingSuppliers);
}