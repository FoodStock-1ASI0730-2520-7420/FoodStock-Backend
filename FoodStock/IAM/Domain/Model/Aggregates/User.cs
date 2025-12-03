namespace FoodStock.IAM.Domain.Model.Aggregates;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Phone { get; private set; }
    public string Segment { get; private set; }
    public string ProfilePicture { get; private set; }
    public string Plan { get; private set; }

    public User(string name, string email, string password, string phone, string segment, string profilePicture)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
        Segment = segment;
        ProfilePicture = profilePicture;
        Plan = null;
    }

    public void SelectPlan(string plan)
    {
        Plan = plan;
    }
}