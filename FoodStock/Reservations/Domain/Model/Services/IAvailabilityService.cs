// path: src/Reservations/Domain/Services/IAvailabilityService.cs
using FoodStock.Reservations.Domain.Model.Aggregates;

namespace FoodStock.Reservations.Domain.Services;

public interface IAvailabilityService
{
    bool IsTableAvailable(IEnumerable<Reservation> sameDayReservations, string time, int durationMinutes);
}

public class AvailabilityService : IAvailabilityService
{
    private static int ToMinutes(string hhmm)
    {
        var parts = hhmm.Split(':');
        var h = int.Parse(parts[0]); var m = int.Parse(parts[1]);
        return h * 60 + m;
    }
    private static bool Overlaps(int aStart, int aEnd, int bStart, int bEnd)
        => aStart < bEnd && bStart < aEnd;

    public bool IsTableAvailable(IEnumerable<Reservation> sameDayReservations, string time, int durationMinutes)
    {
        var s1 = ToMinutes(time);
        var e1 = s1 + durationMinutes;
        foreach (var r in sameDayReservations.Where(r => r.Status != "canceled"))
        {
            var s2 = ToMinutes(r.ReservationTime);
            var e2 = s2 + r.DurationMinutes;
            if (Overlaps(s1, e1, s2, e2)) return false;
        }
        return true;
    }
}