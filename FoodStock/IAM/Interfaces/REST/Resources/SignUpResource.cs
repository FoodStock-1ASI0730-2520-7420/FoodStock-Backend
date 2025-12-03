namespace FoodStock.IAM.Interfaces.REST.Resources;

public class SignUpResource
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Segment { get; set; } = default!;
    public string ProfilePicture { get; set; } = default!;
}