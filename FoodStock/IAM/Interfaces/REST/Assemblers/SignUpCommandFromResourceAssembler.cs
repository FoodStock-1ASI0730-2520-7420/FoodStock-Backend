using FoodStock.IAM.Domain.Model.Commands;
using FoodStock.IAM.Interfaces.REST.Resources;

namespace FoodStock.IAM.Interfaces.REST.Assemblers;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommand(SignUpResource r) => new SignUpCommandImpl
    {
        Name = r.Name,
        Email = r.Email,
        Password = r.Password,
        Phone = r.Phone,
        Segment = r.Segment,
        ProfilePicture = r.ProfilePicture
    };

    private class SignUpCommandImpl : SignUpCommand { }
}