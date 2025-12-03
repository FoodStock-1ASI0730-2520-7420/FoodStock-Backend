namespace FoodStock.IAM.Domain.Model.Commands;

public abstract class SignUpCommand
{
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
    public string Phone { get; init; } = default!;
    public string Segment { get; init; } = default!;
    public string ProfilePicture { get; init; } = default!;
}