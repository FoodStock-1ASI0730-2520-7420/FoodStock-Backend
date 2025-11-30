using FoodStock.Inventory.Domain.Model.Commands.Products;
using FoodStock.Inventory.Interfaces.REST.Resources.Products;

namespace FoodStock.Inventory.Interfaces.REST.Transform.Products;
public static class EditProductCommandFromResourceAssembler
{
    public static EditProductCommand ToCommand(long id, EditProductResource r) =>
        new(id, r.Name, r.UnitPrice, r.Quantity, r.ExpirationDate, r.Category);
}