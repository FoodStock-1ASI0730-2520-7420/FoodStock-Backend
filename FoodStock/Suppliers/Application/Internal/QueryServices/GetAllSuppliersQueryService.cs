using FoodStock.Suppliers.Domain.Model.Aggregate;
using FoodStock.Suppliers.Domain.Repositories;

namespace FoodStock.Suppliers.Application.Internal.QueryServices;

public class GetAllSuppliersQueryService
{
    private readonly ISupplierRepository _repository;

    public GetAllSuppliersQueryService(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Supplier>> Handle()
    {
        return await _repository.GetAllAsync();
    }
}