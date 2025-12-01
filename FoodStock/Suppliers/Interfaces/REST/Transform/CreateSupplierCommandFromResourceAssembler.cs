using FoodStock.Suppliers.Domain.Model.Commands;
using FoodStock.Suppliers.Interfaces.REST.Resources;

namespace FoodStock.Suppliers.Interfaces.REST.Transform;

public static class CreateSupplierCommandFromResourceAssembler
{
    public static CreateSupplierCommand ToCommand(CreateSupplierResource resource) =>
        new(resource.Name, resource.Contact, resource.Type, resource.Email);
}