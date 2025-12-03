using FoodStock.IAM.Domain.Model.Commands;
using FoodStock.IAM.Interfaces.REST.Resources;

namespace FoodStock.IAM.Interfaces.REST.Assemblers;

public static class SelectPlanCommandFromResourceAssembler
{
    public static SelectPlanCommand ToCommand(SelectPlanResource r) => new SelectPlanCommandImpl
    {
        UserId = r.UserId,
        Plan = r.Plan
    };

    private class SelectPlanCommandImpl : SelectPlanCommand { }
}