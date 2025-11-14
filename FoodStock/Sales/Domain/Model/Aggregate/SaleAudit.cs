using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStock.Sales.Domain.Model.Aggregate;

public partial class SaleAudit
{
    [Column("CreationDate")] public DateTime CreationDate { get; set; }
    [Column("CreationTime")] public TimeSpan CreationTime { get; set; }

    public SaleAudit()
    {
        CreationDate = DateTime.Now;
        CreationTime = DateTime.Now.TimeOfDay;
    }
}