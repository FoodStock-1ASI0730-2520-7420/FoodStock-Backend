namespace FoodStock.IAM.Domain.Model.Commands;

public abstract class SignInCommand
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}