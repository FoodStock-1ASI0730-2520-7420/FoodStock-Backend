using FoodStock.IAM.Domain.Model.Aggregates;
using FoodStock.IAM.Domain.Model.Commands;

namespace FoodStock.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<User> Handle(SignUpCommand command);
    Task<User> Handle(SelectPlanCommand command);
}