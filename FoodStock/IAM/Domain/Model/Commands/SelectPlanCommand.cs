namespace FoodStock.IAM.Domain.Model.Commands;

public abstract class SelectPlanCommand
{
    public Guid UserId { get; init; }
    public string Plan { get; init; } = default!;
}