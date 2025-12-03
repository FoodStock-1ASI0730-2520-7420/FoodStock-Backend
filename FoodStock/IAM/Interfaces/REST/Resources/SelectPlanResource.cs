namespace FoodStock.IAM.Interfaces.REST.Resources;

public class SelectPlanResource
{
    public Guid UserId { get; set; }
    public string Plan { get; set; } = default!;
}