using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.Commands;
using FoodStock.Sales.Domain.Repositories;
using FoodStock.Sales.Domain.Services;
using FoodStock.Shared.Domain.Repositories;

namespace FoodStock.Sales.Application.Internal.CommandServices;

public class SaleCommandService(
    ISaleRepository saleRepository,
    IDishRepository dishRepository,
    IUnitOfWork unitOfWork) : ISaleCommandService
{
    public async Task<Sale?> Handle(CreateSaleCommand command)
    {
        var saleItems = new List<SaleItem>();
        foreach (CreateSaleItemCommand itemCommand in command.SaleItems)
        {
            var dish = await dishRepository.FindByIdAsync(itemCommand.DishId);
            if(dish is null)
                throw new Exception("Dish with id {itemCommand.DishId} not found");

            var saleItem = new SaleItem(itemCommand, dish, 0);
            saleItems.Add(saleItem);
        }

        var sale = new Sale(
            command.SaleType,
            command.PaymentMethod,
            command.Waiter,
            saleItems);
        await saleRepository.AddAsync(sale);
        await unitOfWork.CompleteAsync();
        return sale;
    }
}