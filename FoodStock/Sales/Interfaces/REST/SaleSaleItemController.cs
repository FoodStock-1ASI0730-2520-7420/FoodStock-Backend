using System.Net.Mime;
using FoodStock.Sales.Domain.Model.Queries;
using FoodStock.Sales.Domain.Services;
using FoodStock.Sales.Interfaces.REST.Resources;
using FoodStock.Sales.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodStock.Sales.Interfaces.REST;

[ApiController]
[Route("api/v1/sale/{saleId:int}/saleItems")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("SaleItems")]
public class SaleSaleItemController(ISaleItemQueryService saleItemQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all sale items by sale id",
        Description = "Get all sale items by sale id",
        OperationId = "GetAllSaleItemsBySaleId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The sale were found", typeof(IEnumerable<SaleItemResource>))]
    public async Task<IActionResult> GetSaleItemsBySaleId([FromRoute] int saleId)
    {
        var getSaleItemsBySaleIdQuery = new GetAllSaleItemsBySaleIdQuery(saleId);
        var saleItems = await saleItemQueryService.Handle(getSaleItemsBySaleIdQuery);
        var saleItemResources = saleItems.Select(SaleItemResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(saleItemResources);
    }
}