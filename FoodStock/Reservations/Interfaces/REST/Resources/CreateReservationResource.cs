// path: src/Reservations/Interfaces/REST/Resources/CreateReservationResource.cs
namespace FoodStock.Reservations.Interfaces.REST.Resources;

public record CreateReservationResource(
    int TableNumber,
    int QuantityPeople,
    string ReservationDate,
    string ReservationTime,
    int DurationMinutes,
    string CustomerName,
    string CustomerPhone);