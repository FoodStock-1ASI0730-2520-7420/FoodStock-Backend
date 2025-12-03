using FoodStock.IAM.Domain.Model.Commands;
using FoodStock.IAM.Interfaces.REST.Resources;

namespace FoodStock.IAM.Interfaces.REST.Assemblers;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommand(SignInResource r) => new SignInCommandImpl
    {
        Email = r.Email,
        Password = r.Password
    };

    private class SignInCommandImpl : SignInCommand { }
}