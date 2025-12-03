namespace FoodStock.IAM.Interfaces.REST.Resources;

public class SignInResource
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}