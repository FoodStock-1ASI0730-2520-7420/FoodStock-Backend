using System.Text.RegularExpressions;
using FoodStock.Suppliers.Domain.Model.Aggregate;

namespace FoodStock.Suppliers.Domain.Services;

public class SupplierDomainService : ISupplierDomainService
{
    public bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public bool IsDuplicate(Supplier supplier, IEnumerable<Supplier> existingSuppliers)
    {
        return existingSuppliers.Any(s => s.Email == supplier.Email && s.Id != supplier.Id);
    }
}