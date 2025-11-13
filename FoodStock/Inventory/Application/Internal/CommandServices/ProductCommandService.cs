using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Commands.Products;
using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Inventory.Domain.Services;
using FoodStock.Shared.Domain.Repositories;

namespace FoodStock.Inventory.Application.Internal.CommandServices;
public class ProductCommandService : IProductCommandService
{
    private readonly IProductRepository _repo;
    private readonly IUnitOfWork _uow;
    public ProductCommandService(IProductRepository repo, IUnitOfWork uow) { _repo = repo; _uow = uow; }

    public async Task<Product> Handle(CreateProductCommand command)
    {
        var entity = new Product(command.Name, command.UnitPrice, command.Quantity, command.ExpirationDate, command.Category);
        await _repo.AddAsync(entity);
        await _uow.CompleteAsync();
        return entity;
    }

    public async Task<Product> Handle(EditProductCommand command)
    {
        var entity = await _repo.FindByIdAsync(command.Id) ?? throw new KeyNotFoundException("Product not found");
        entity.Edit(command.Name, command.UnitPrice, command.Quantity, command.ExpirationDate, command.Category);
        _repo.Update(entity);
        await _uow.CompleteAsync();
        return entity;
    }

    public async Task Handle(DeleteProductCommand command)
    {
        var entity = await _repo.FindByIdAsync(command.Id) ?? throw new KeyNotFoundException("Product not found");
        entity.Delete();
        _repo.Update(entity);
        await _uow.CompleteAsync();
    }

    public async Task<Product> Handle(ConsumeProductCommand command)
    {
        var entity = await _repo.FindByIdAsync(command.Id) ?? throw new KeyNotFoundException("Product not found");
        entity.Consume(command.Amount);
        _repo.Update(entity);
        await _uow.CompleteAsync();
        return entity;
    }
}