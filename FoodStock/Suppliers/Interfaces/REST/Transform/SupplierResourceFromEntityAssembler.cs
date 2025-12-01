using FoodStock.Suppliers.Domain.Model.Aggregate;
using FoodStock.Suppliers.Interfaces.REST.Resources;

namespace FoodStock.Suppliers.Interfaces.REST.Transform;

public static class SupplierResourceFromEntityAssembler
{
    public static SupplierResource ToResource(Supplier entity) =>
        new(entity.Id, entity.Name, entity.Contact, entity.Type, entity.Email);
}