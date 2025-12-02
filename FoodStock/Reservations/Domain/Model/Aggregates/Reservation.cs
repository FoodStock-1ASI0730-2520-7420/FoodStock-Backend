namespace FoodStock.Reservations.Domain.Model.Aggregates;

public class Reservation
{
    public int Id { get; private set; }
    public int TableNumber { get; private set; }   // clave natural para relacionar con Table.Number
    public int QuantityPeople { get; private set; }
    public string ReservationDate { get; private set; } // "YYYY-MM-DD" (alineado al frontend)
    public string ReservationTime { get; private set; } // "HH:MM"
    public string CreationDate { get; private set; }    // "YYYY-MM-DD"
    public string CreationTime { get; private set; }    // "HH:MM"
    public int DurationMinutes { get; private set; }    // default 120
    public string Status { get; private set; }          // scheduled | active | completed | canceled
    public string CustomerName { get; private set; } = string.Empty;
    public string CustomerPhone { get; private set; } = string.Empty;

    // EF
    private Reservation() { }

    public Reservation(
        int tableNumber,
        int quantityPeople,
        string reservationDate,
        string reservationTime,
        int durationMinutes,
        string customerName,
        string customerPhone,
        string? status = null,
        string? creationDate = null,
        string? creationTime = null)
    {
        if (string.IsNullOrWhiteSpace(reservationDate)) throw new ArgumentException("reservationDate required");
        if (string.IsNullOrWhiteSpace(reservationTime)) throw new ArgumentException("reservationTime required");
        if (durationMinutes <= 0) throw new ArgumentOutOfRangeException(nameof(durationMinutes));
        if (quantityPeople <= 0) throw new ArgumentOutOfRangeException(nameof(quantityPeople));
        if (tableNumber <= 0) throw new ArgumentOutOfRangeException(nameof(tableNumber));

        TableNumber = tableNumber;
        QuantityPeople = quantityPeople;
        ReservationDate = reservationDate;
        ReservationTime = reservationTime;
        DurationMinutes = durationMinutes;
        CustomerName = customerName ?? string.Empty;
        CustomerPhone = customerPhone ?? string.Empty;
        Status = string.IsNullOrWhiteSpace(status) ? "scheduled" : status;
        CreationDate = creationDate ?? DateTime.UtcNow.ToString("yyyy-MM-dd");
        CreationTime = creationTime ?? DateTime.UtcNow.ToString("HH:mm");
    }

    public void Update(
        int tableNumber,
        int quantityPeople,
        string reservationDate,
        string reservationTime,
        int durationMinutes,
        string? status,
        string? customerName,
        string? customerPhone)
    {
        if (durationMinutes <= 0) throw new ArgumentOutOfRangeException(nameof(durationMinutes));
        if (quantityPeople <= 0) throw new ArgumentOutOfRangeException(nameof(quantityPeople));
        if (tableNumber <= 0) throw new ArgumentOutOfRangeException(nameof(tableNumber));
        if (string.IsNullOrWhiteSpace(reservationDate)) throw new ArgumentException("reservationDate required");
        if (string.IsNullOrWhiteSpace(reservationTime)) throw new ArgumentException("reservationTime required");

        TableNumber = tableNumber;
        QuantityPeople = quantityPeople;
        ReservationDate = reservationDate;
        ReservationTime = reservationTime;
        DurationMinutes = durationMinutes;
        if (!string.IsNullOrWhiteSpace(status)) Status = status!;
        if (customerName is not null) CustomerName = customerName;
        if (customerPhone is not null) CustomerPhone = customerPhone;
    }

    public void Cancel() => Status = "canceled";
}