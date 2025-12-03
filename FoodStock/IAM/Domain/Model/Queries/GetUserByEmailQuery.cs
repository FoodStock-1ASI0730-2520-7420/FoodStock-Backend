namespace FoodStock.IAM.Domain.Model.Queries;

public abstract class GetUserByEmailQuery
{
    public string Email { get; init; } = default!;
}