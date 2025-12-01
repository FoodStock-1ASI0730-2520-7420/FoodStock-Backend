using FoodStock.Suppliers.Domain.Model.Aggregate;
using FoodStock.Suppliers.Domain.Repositories;

namespace FoodStock.Suppliers.Application.Internal.QueryServices;

public class GetSupplierByIdQueryService
{
    private readonly ISupplierRepository _repository;

    public GetSupplierByIdQueryService(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<Supplier?> Handle(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}