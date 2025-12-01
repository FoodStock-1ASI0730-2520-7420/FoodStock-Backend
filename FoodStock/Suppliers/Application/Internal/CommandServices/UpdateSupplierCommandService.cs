using FoodStock.Suppliers.Domain.Model.Aggregate;
using FoodStock.Suppliers.Domain.Model.Commands;
using FoodStock.Suppliers.Domain.Repositories;

namespace FoodStock.Suppliers.Application.Internal.CommandServices;

public class UpdateSupplierCommandService
{
    private readonly ISupplierRepository _repository;

    public UpdateSupplierCommandService(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<Supplier?> Handle(UpdateSupplierCommand command)
    {
        var existing = await _repository.GetByIdAsync(command.Id);
        if (existing is null) return null;

        existing.Update(command);
        return await _repository.UpdateAsync(existing);
    }
}