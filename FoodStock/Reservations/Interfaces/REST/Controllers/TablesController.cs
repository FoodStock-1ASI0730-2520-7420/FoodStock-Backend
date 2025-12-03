// path: src/Reservations/Interfaces/REST/Controllers/TablesController.cs
using System.Net.Mime;
using FoodStock.Reservations.Application.Internal.CommandServices;
using FoodStock.Reservations.Application.Internal.QueryServices;
using FoodStock.Reservations.Interfaces.REST.Resources;
using FoodStock.Reservations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // <-- IMPORTANTE para DbUpdateException
using Swashbuckle.AspNetCore.Annotations;

namespace FoodStock.Reservations.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/tables")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Tables")]
public class TablesController(TableQueryService query, TableCommandService commands) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List tables", OperationId = "ListTables")]
    [SwaggerResponse(StatusCodes.Status200OK, "Tables found", typeof(IEnumerable<TableResource>))]
    public async Task<IActionResult> ListAsync()
        => Ok((await query.ListAsync()).Select(TableResourceAssembler.ToResource));

    [HttpPost]
    [SwaggerOperation(Summary = "Create table", OperationId = "CreateTable")]
    [SwaggerResponse(StatusCodes.Status201Created, "Table created", typeof(TableResource))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTableResource body)
    {
        try
        {
            var created = await commands.CreateAsync(body.Number, body.Capacity);
            var res = TableResourceAssembler.ToResource(created);

            // Location manual para evitar 500 si no hay GetById
            var location = $"{Request.Path}/{res.Id}";
            return Created(location, res);
        }
        // Específicas primero (valores fuera de rango)
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        // Duplicados/constraints desde EF (índice único, etc.)
        catch (DbUpdateException ex)
        {
            // Mensaje genérico como conflicto; evita 500
            return Conflict(new { error = "La mesa ya existe o hay un conflicto de datos.", detail = ex.InnerException?.Message ?? ex.Message });
        }
        // Reglas de dominio (p.ej. número ya existe, validaciones previas)
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        // Más general al final (argumentos inválidos)
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete table", OperationId = "DeleteTable")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Table deleted")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await commands.DeleteAsync(id);
        return NoContent();
    }
}
