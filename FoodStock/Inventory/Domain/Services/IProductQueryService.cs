using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Queries.Products;

namespace FoodStock.Inventory.Domain.Services;
public interface IProductQueryService
{
    Task<Product?> Handle(GetProductByIdQuery query);
    Task<List<Product>> Handle(GetAllProductsQuery query);
}