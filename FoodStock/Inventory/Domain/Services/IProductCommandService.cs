using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Commands.Products;

namespace FoodStock.Inventory.Domain.Services;
public interface IProductCommandService
{
    Task<Product> Handle(CreateProductCommand command);
    Task<Product> Handle(EditProductCommand command);
    Task Handle(DeleteProductCommand command);
    Task<Product> Handle(ConsumeProductCommand command);
}