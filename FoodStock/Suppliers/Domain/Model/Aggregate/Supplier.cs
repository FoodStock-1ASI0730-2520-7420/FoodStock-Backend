using FoodStock.Suppliers.Domain.Model.Commands;

namespace FoodStock.Suppliers.Domain.Model.Aggregate;

public partial class Supplier
{
    // EF Core necesita constructor vacío
    public Supplier() { }

    public Supplier(string name, string contact, string type, string email)
    {
        Name = name;
        Contact = contact;
        Type = type;
        Email = email;
    }

    // Constructor rico desde CreateSupplierCommand
    public Supplier(CreateSupplierCommand command)
        : this(command.Name, command.Contact, command.Type, command.Email)
    {
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
    public string Contact { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    // Mutación controlada desde UpdateSupplierCommand
    public void Update(UpdateSupplierCommand command)
    {
        Name = command.Name;
        Contact = command.Contact;
        Type = command.Type;
        Email = command.Email;
    }
}