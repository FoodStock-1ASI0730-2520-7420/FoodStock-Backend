// path: src/Reservations/Interfaces/REST/Resources/UpdateReservationResource.cs
namespace FoodStock.Reservations.Interfaces.REST.Resources;

public record UpdateReservationResource(
    int TableNumber,
    int QuantityPeople,
    string ReservationDate,
    string ReservationTime,
    int DurationMinutes,
    string? Status,
    string? CustomerName,
    string? CustomerPhone);