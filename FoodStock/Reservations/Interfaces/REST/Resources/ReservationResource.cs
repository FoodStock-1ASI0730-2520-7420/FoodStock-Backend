// path: src/Reservations/Interfaces/REST/Resources/ReservationResource.cs
namespace FoodStock.Reservations.Interfaces.REST.Resources;

public record ReservationResource(
    int Id,
    int TableNumber,
    int QuantityPeople,
    string ReservationDate,
    string ReservationTime,
    string CreationDate,
    string CreationTime,
    int DurationMinutes,
    string Status,
    string CustomerName,
    string CustomerPhone);