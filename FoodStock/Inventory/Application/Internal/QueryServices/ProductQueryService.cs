using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Queries.Products;
using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Inventory.Domain.Services;

namespace FoodStock.Inventory.Application.Internal.QueryServices;
public class ProductQueryService : IProductQueryService
{
    private readonly IProductRepository _repo;
    public ProductQueryService(IProductRepository repo) => _repo = repo;

    public Task<Product?> Handle(GetProductByIdQuery query) => _repo.FindByIdAsync(query.Id);
    public Task<List<Product>> Handle(GetAllProductsQuery query) => _repo.ListAsync();
}