namespace FoodStock.Reservations.Domain.Model.Aggregates;

public class Table
{
    public int Id { get; private set; }
    public int Number { get; private set; }
    public int Capacity { get; private set; }

    // EF
    private Table() { }

    public Table(int number, int capacity)
    {
        if (capacity < 2 || capacity > 10) throw new ArgumentOutOfRangeException(nameof(capacity));
        if (number < 1) throw new ArgumentOutOfRangeException(nameof(number));
        Number = number;
        Capacity = capacity;
    }

    public void UpdateCapacity(int capacity)
    {
        if (capacity < 2 || capacity > 10) throw new ArgumentOutOfRangeException(nameof(capacity));
        Capacity = capacity;
    }
}