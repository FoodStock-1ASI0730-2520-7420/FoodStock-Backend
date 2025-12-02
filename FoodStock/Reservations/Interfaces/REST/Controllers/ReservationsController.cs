// path: src/Reservations/Interfaces/REST/Controllers/ReservationsController.cs
using System.Net.Mime;
using FoodStock.Reservations.Application.Internal.CommandServices;
using FoodStock.Reservations.Application.Internal.QueryServices;
using FoodStock.Reservations.Interfaces.REST.Resources;
using FoodStock.Reservations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodStock.Reservations.Interfaces.REST.Controllers;

[ApiController]
[Route("reservations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Reservations")]
public class ReservationsController(ReservationQueryService query, ReservationCommandService commands) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List reservations", OperationId = "ListReservations")]
    [SwaggerResponse(StatusCodes.Status200OK, "Reservations found", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> ListAsync()
        => Ok((await query.ListAsync()).Select(ReservationResourceAssembler.ToResource));

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get reservation by id", OperationId = "GetReservationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Reservation found", typeof(ReservationResource))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var entity = await query.FindByIdAsync(id);
        if (entity is null) return NotFound();
        return Ok(ReservationResourceAssembler.ToResource(entity));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create reservation", OperationId = "CreateReservation")]
    [SwaggerResponse(StatusCodes.Status201Created, "Reservation created", typeof(ReservationResource))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateReservationResource body)
    {
        var created = await commands.CreateAsync(
            body.TableNumber, body.QuantityPeople, body.ReservationDate, body.ReservationTime,
            body.DurationMinutes, body.CustomerName, body.CustomerPhone);

        var res = ReservationResourceAssembler.ToResource(created);
        return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update reservation", OperationId = "UpdateReservation")]
    [SwaggerResponse(StatusCodes.Status200OK, "Reservation updated", typeof(ReservationResource))]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateReservationResource body)
    {
        var updated = await commands.UpdateAsync(
            id,
            body.TableNumber, body.QuantityPeople, body.ReservationDate, body.ReservationTime,
            body.DurationMinutes, body.Status, body.CustomerName, body.CustomerPhone);

        return Ok(ReservationResourceAssembler.ToResource(updated));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete reservation", OperationId = "DeleteReservation")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Reservation deleted")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await commands.DeleteAsync(id);
        return NoContent();
    }
}