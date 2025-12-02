// path: src/Reservations/Interfaces/REST/Transform/ReservationResourceAssembler.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using FoodStock.Reservations.Interfaces.REST.Resources;

namespace FoodStock.Reservations.Interfaces.REST.Transform;

public static class ReservationResourceAssembler
{
    public static ReservationResource ToResource(Reservation e) =>
        new(e.Id, e.TableNumber, e.QuantityPeople, e.ReservationDate, e.ReservationTime,
            e.CreationDate, e.CreationTime, e.DurationMinutes, e.Status, e.CustomerName, e.CustomerPhone);
}