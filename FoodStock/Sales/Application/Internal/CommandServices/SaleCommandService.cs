using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.Commands;
using FoodStock.Sales.Domain.Repositories;
using FoodStock.Sales.Domain.Services;
using FoodStock.Shared.Domain.Repositories;

namespace FoodStock.Sales.Application.Internal.CommandServices;

public class SaleCommandService(
    ISaleRepository saleRepository,
    ISaleItemRepository saleItemRepository,
    IDishRepository dishRepository,
    IProductRepository productRepository,
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

        foreach (var saleItem in sale.SaleItems)
        {
            var dish = await dishRepository.FindByIdAsync(saleItem.DishId);
            if(dish is null)
                throw new Exception("Dish with id {saleItem.DishId} not found");

            foreach (var ingredient in dish.Ingredients)
            {
                var product = await productRepository.FindByIdAsync(ingredient.ProductId);
                if (product is null)
                    throw new Exception("Product with id {ingredient.ProductId} not found");

                var totalToConsume = ingredient.Quantity * saleItem.Quantity;
                product.Consume(totalToConsume);
            }
        }
        await unitOfWork.CompleteAsync();
        return sale;
    }

    public async Task<Sale?> Handle(UpdateSaleCommand command)
    {
        var sale = await saleRepository.FindByIdWithSaleItemsAsync(command.Id);
        if (sale is null)
            throw new Exception("Sale with id {command.Id} not found");
        
        sale.UpdateDetails(command.SaleType, command.PaymentMethod, command.Waiter);
        
        var originalItems = sale.SaleItems.ToList();

        foreach (var item in sale.SaleItems.ToList())
        {
            saleItemRepository.Remove(item);
        }
        
        sale.ClearSaleItems();

        foreach (var item in command.SaleItems)
        {
            var dish = await dishRepository.FindByIdAsync(item.DishId);
            if (dish is null)
                throw new Exception($"Dish with id {item.DishId} not found");
            
            var saleItem = new SaleItem(
                dish.Id,
                dish.Name,
                dish.PriceUnit,
                item.Quantity,
                sale.Id);
            sale.AddSaleItem(saleItem);
        }
        
        foreach (var newItem in sale.SaleItems)
        {
            var dish = await dishRepository.FindByIdAsync(newItem.DishId);
            if(dish is null)
                throw new Exception("Dish with id {newItem.DishId} not found");
            
            var originalItem = originalItems.FirstOrDefault(i => i.DishId == newItem.DishId);
            var originalQuantity = originalItem?.Quantity ?? 0;
            int difference = newItem.Quantity - originalQuantity;

            foreach (var ingredient in dish.Ingredients)
            {
                var product = await productRepository.FindByIdAsync(ingredient.ProductId);
                if (product is null)
                    throw new Exception("Product with id {ingredient.ProductId} not found");
                
                var totalDifference = ingredient.Quantity * difference;
                if (difference > 0)
                    product.Consume(totalDifference);
                else if(difference < 0)
                    product.AddStock(Math.Abs(totalDifference));
            }
        }
        await unitOfWork.CompleteAsync();
        return sale;
    }

    public async Task Handle(DeleteSaleCommand command)
    {
        var sale = await saleRepository.FindByIdWithSaleItemsAsync(command.Id);
        if(sale is null)
            throw new Exception("Sale with id {command.Id} not found");

        foreach (var item in sale.SaleItems.ToList())
        {
            saleItemRepository.Remove(item);
        }

        saleRepository.Remove(sale);
        await unitOfWork.CompleteAsync();
    }
}